#pragma once

#include <string>

namespace OAJ::Compiler::Models
{
	class LanguageDescriptor
	{
	private:
		std::string m_program;
		std::string m_callable;
		std::string m_extension;
	public:
		LanguageDescriptor() = delete;
		LanguageDescriptor(std::string program, std::string callable, std::string extension);

		auto getProgram() -> std::string;
		auto getCallable() -> std::string;
		auto getExtension() -> std::string;
	};
}
