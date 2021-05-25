#include "..\applicationmessages.h"

std::string OAJ::ApplicationMessages::getPortNumberMessage(int port)
{
	return "The Judge is running under port: " + std::to_string(port);
}
