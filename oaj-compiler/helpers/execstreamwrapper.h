#pragma once

#include <string>

namespace OAJ::Compiler::Helpers
{
	class ExecStreamWrapper
	{
	public:
		static auto execute(const std::string& program, const std::string& arguments) -> std::pair<std::string, std::string>;
	};
}
