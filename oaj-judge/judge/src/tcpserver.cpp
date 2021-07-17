#include "judge/tcpserver.h"

#include "compiler/compiler.h"

#undef UNICODE

#define WIN32_LEAN_AND_MEAN

#include <windows.h>
#include <winsock2.h>
#include <stdlib.h>
#include <stdio.h>
#include <iostream>

#include "judge/applicationmessages.h"

// Need to link with Ws2_32.lib
#pragma comment (lib, "Ws2_32.lib")

#define DEFAULT_BUFLEN 1024

auto OAJ::Judge::Communication::TcpServer::logPort() -> void
{
    struct sockaddr_in sin;
    auto len = socklen_t{ sizeof(sin) };
    if (getsockname(m_listenSocket, (struct sockaddr*)&sin, &len) != -1)
        std::cout << OAJ::Judge::ApplicationMessages::getPortNumberMessage(ntohs(sin.sin_port));
}

auto OAJ::Judge::Communication::TcpServer::closeAfterFailedOperation(const std::string& operationName, int lastError) -> int
{
    std::cout << operationName << " failed with error: " << lastError << "\n";
    WSACleanup();
    return 1;
}

auto OAJ::Judge::Communication::TcpServer::start(PCSTR port) -> int
{
    WSADATA wsaData;

    int iSendResult;
    char recvbuf[DEFAULT_BUFLEN];
    int recvbuflen = DEFAULT_BUFLEN;

    // Initialize Winsock
    m_iResult = WSAStartup(MAKEWORD(2, 2), &wsaData);
    if (m_iResult != 0)
    {
        return closeAfterFailedOperation("WSAStartup", m_iResult);
    }

    ZeroMemory(&m_hints, sizeof(m_hints));
    m_hints.ai_family = AF_INET;
    m_hints.ai_socktype = SOCK_STREAM;
    m_hints.ai_protocol = IPPROTO_TCP;
    m_hints.ai_flags = AI_PASSIVE;

    // Resolve the server address and port
    m_iResult = getaddrinfo(NULL, port, &m_hints, &m_result);
    if (m_iResult != 0)
        return closeAfterFailedOperation("getaddrinfo", m_iResult);

    // Create a SOCKET for connecting to server
    m_listenSocket = socket(m_result->ai_family, m_result->ai_socktype, m_result->ai_protocol);
    if (m_listenSocket == INVALID_SOCKET)
        return closeAfterFailedOperation("socket", WSAGetLastError());

    // Setup the TCP listening socket
    m_iResult = bind(m_listenSocket, m_result->ai_addr, static_cast<int>(m_result->ai_addrlen));
    if (m_iResult == SOCKET_ERROR)
        return closeAfterFailedOperation("bind", WSAGetLastError());

    freeaddrinfo(m_result);

    m_iResult = listen(m_listenSocket, SOMAXCONN);
    if (m_iResult == SOCKET_ERROR)
        return closeAfterFailedOperation("listen", WSAGetLastError());

    logPort();

    // Accept a client socket
    m_clientSocket = accept(m_listenSocket, NULL, NULL);
    if (m_clientSocket == INVALID_SOCKET)
        return closeAfterFailedOperation("accept", WSAGetLastError());

    // No longer need server socket
    closesocket(m_listenSocket);

    // Receive until the peer shuts down the connection
    recvbuflen = 1;
    auto twice = 0;
    auto once = false;
    auto results = std::pair<std::string, std::string>{};
    do {
        m_iResult = recv(m_clientSocket, recvbuf, recvbuflen, 0);
        if (m_iResult > 0) {
            printf("\nBytes received: %d\n", m_iResult);
            recvbuflen = DEFAULT_BUFLEN;

            // Echo the buffer back to the sender
            iSendResult = send(m_clientSocket, recvbuf, m_iResult, 0);

            if (iSendResult == SOCKET_ERROR)
                return closeAfterFailedOperation("send", WSAGetLastError());

            //
            if (!once)
            {
                once = true;
                auto compiler = OAJ::Compiler::Compiler("C:\\Users\\User\\Desktop\\oaj\\oaj-compiler\\resources\\supported_languages.xml");
                results = compiler.process("Cpp", L"#include <iostream>\nusing namespace std;\nint main()\n{\nstd::cout << \"Hello G++!\\nBye G++!\";\nreturn 1;\n}");
            }
            if (twice % 2)
            {
                std::cout << "Output:\n" << results.first;
                std::cout << "\nErrors:\n" << results.second;
            }
            ++twice;
            //
            //printf("Bytes sent: %d\n", iSendResult);
        }
        else if (m_iResult == 0)
        {
            printf("Connection closing...\n");
        }
        else
        {
            return 1; // closeAfterFailedOperation("recv", WSAGetLastError());
        }

    } while (m_iResult > 0);

    return 0;
}

auto OAJ::Judge::Communication::TcpServer::stop() -> int
{
    // shutdown the connection since we're done
    m_iResult = shutdown(m_clientSocket, SD_SEND);
    if (m_iResult == SOCKET_ERROR)
        return closeAfterFailedOperation("shutdown", WSAGetLastError());

    freeaddrinfo(m_result);
    closesocket(m_clientSocket);
    WSACleanup();

    return 0;
}