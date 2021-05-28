#include "models/languagedescriptor.h"

OAJ::Compiler::Models::LanguageDescriptor::LanguageDescriptor(std::string program, std::string callable, std::string extension)
	: m_program{program}
	, m_callable{callable}
	, m_extension{extension}
{
}

auto OAJ::Compiler::Models::LanguageDescriptor::getProgram()->std::string
{
	return m_program;
}

auto OAJ::Compiler::Models::LanguageDescriptor::getCallable()->std::string
{
	return m_callable;
}

auto OAJ::Compiler::Models::LanguageDescriptor::getExtension()->std::string
{
	return m_extension;
}