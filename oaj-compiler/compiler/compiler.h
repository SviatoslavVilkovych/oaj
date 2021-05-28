#pragma once
#include "models/languagedescriptor.h"

#include <map>
#include <string>

namespace OAJ::Compiler
{
	class Compiler
	{
	private:
		std::map<std::string, OAJ::Compiler::Models::LanguageDescriptor> m_languageCodeToLanguageDescriptor;
		
		// Writes program to file with appopriate extension.
		auto writeProgramToTempFile(const std::string& ext, const std::wstring& program) -> std::wstring;
		auto removeTempFile(const std::wstring& tempFileName) -> bool;
		auto compileAndGetOutput(const std::wstring& fileName, const std::string& program, const std::string& extension) -> std::pair<std::string, std::string>;
		auto runAndGetOutput(const std::wstring& fileName, const std::string& callableExtension) -> std::pair<std::string, std::string>;
	public:
		Compiler(const char* filename);
		auto process(const std::string& languageName, const std::wstring& content) ->std::pair<std::string, std::string>;
	};
}
