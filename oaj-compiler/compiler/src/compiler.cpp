#include "compiler/compiler.h"

#include "helpers/stringconverter.h"

#include "dependencies/tinyxml2/tinyxml2.h"

#include <fstream>
#include <filesystem>

OAJ::Compiler::Compiler::Compiler(const char* filename)
{
	auto doc = tinyxml2::XMLDocument{};
	doc.LoadFile(filename);
	
	auto languages = doc.FirstChildElement();
	auto lCode = std::string{};
	for (auto l = languages->FirstChildElement(); l; l = l->NextSiblingElement())
	{
		m_languageToLanguageExpression.emplace(l->Attribute("code"), std::pair{ l->Attribute("extension"), l->GetText() });
	}
}

auto OAJ::Compiler::Compiler::compile(const std::string& languageName, const std::wstring& program) -> std::wstring
{
	auto expression = m_languageToLanguageExpression.find(languageName);
	if (expression != m_languageToLanguageExpression.end())
	{
		auto fileName = writeProgramToTempFile(std::get<0>(expression->second), program);
		// 2. Execute expression to compile
		// 3. Get executable and retrieve output
	}
	return std::wstring{};
}

auto OAJ::Compiler::Compiler::writeProgramToTempFile(const std::string& ext, const std::wstring& program) -> std::wstring
{
	GUID guid;
	CoCreateGuid(&guid);
	auto fileName = OAJ::Helpers::StringConverter::to_wstring(guid);
	auto extension = OAJ::Helpers::StringConverter::to_wstring(ext);
	fileName.append(extension);
	auto f = std::wofstream{ fileName, std::ios::out };
	f << program;
	f.close();
	return fileName;
}

auto OAJ::Compiler::Compiler::removeTempFile(const std::wstring& tempFileName) -> bool
{
	return std::filesystem::remove(tempFileName);
}