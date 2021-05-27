#pragma once
#include <map>
#include <string>

namespace OAJ::Compiler
{
	class Compiler
	{
	private:
		std::map<std::string, std::pair<std::string, std::string>> m_languageToLanguageExpression;
		
		// Writes program to file with appopriate extension.
		auto writeProgramToTempFile(const std::string& ext, const std::wstring& program) -> std::wstring;
		auto removeTempFile(const std::wstring& tempFileName) -> bool;
	public:
		Compiler(const char* filename);
		auto compile(const std::string& languageName, const std::wstring& program) -> std::wstring;
	};
}
