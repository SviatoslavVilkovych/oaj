#pragma once
#include <ws2tcpip.h>
#include <string>

namespace OAJ::Judge::Communication
{
	class TcpServer
	{
	private:
		SOCKET m_listenSocket = INVALID_SOCKET;
		SOCKET m_clientSocket = INVALID_SOCKET;
		int m_iResult;
		struct addrinfo* m_result = NULL;
		struct addrinfo m_hints;

		auto logPort() -> void;
		auto closeAfterFailedOperation(const std::string& operationName, int lastError) -> int;
	public:
		auto start(PCSTR port) -> int;
		auto stop() -> int;
	};
}