// oaj-compiler.cpp : Defines the functions for the static library.
//

#include "compiler\pch.h"
#include "compiler\compiler.h"

auto OAJ::Compiler::Compiler::compile(std::string languageName, const std::string& program) -> std::string
{
	auto expression = m_languageToLanguageExpression.find(languageName);
	if (expression != m_languageToLanguageExpression.end())
	{
		// 1. Write program to file with appopriate extension
		// 2. Execute expression to compile
		// 3. Get executable and retrieve output
	}
	return std::string{};
}

OAJ::Compiler::Compiler::Compiler(const char* filename)
{
	auto doc = tinyxml2::XMLDocument{};
	doc.LoadFile(filename);

	auto languages = doc.FirstChildElement();
	auto lCode = std::string{};
	for (auto l = languages->FirstChildElement(); l; l = l->NextSiblingElement())
	{
		m_languageToLanguageExpression.emplace(l->Attribute("code"), l->GetText());
	}
}