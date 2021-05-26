#pragma once

#include "compiler\pch.h"

namespace OAJ::Compiler
{
	class Compiler
	{
		std::map<std::string, std::string> m_languageToLanguageExpression;
	public:
		Compiler(const char* filename);
		auto compile(std::string languageName, const std::string& program) -> std::string;
	};
}
