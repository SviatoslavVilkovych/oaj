#include "compiler/compiler.h"

#include "helpers/stringconverter.h"
#include "helpers/workingpathmanipulator.h"
#include "helpers/execstreamwrapper.h"

#include "tinyxml2/tinyxml2.h"

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
		m_languageCodeToLanguageDescriptor.emplace(l->Attribute("code"), OAJ::Compiler::Models::LanguageDescriptor{ l->Attribute("program"), l->Attribute("callable"), l->Attribute("extension") });
	}
}

auto OAJ::Compiler::Compiler::process(const std::string& languageName, const std::wstring& content) -> std::pair<std::string, std::string>
{
	auto expression = m_languageCodeToLanguageDescriptor.find(languageName);
	if (expression != m_languageCodeToLanguageDescriptor.end())
	{
		auto fileName = writeProgramToTempFile(expression->second.getExtension(), content);
		// 2. Execute expression to compile
		auto compileOutput = compileAndGetOutput(fileName, expression->second.getProgram(), expression->second.getExtension());
		// 3. Get executable and retrieve output
		auto runOutput = runAndGetOutput(fileName, expression->second.getCallable());
		// 4. Remove file
		removeTempFile(fileName);
		return runOutput;
	}
	return std::pair<std::string, std::string>{};
}

auto OAJ::Compiler::Compiler::writeProgramToTempFile(const std::string& extension, const std::wstring& program) -> std::wstring
{
	GUID guid;
	CoCreateGuid(&guid);
	auto fileName = OAJ::Compiler::Helpers::StringConverter::to_wstring(guid);
	// GUID wraped with '{','}' characters, which might be not friends with every cmd.
	fileName.pop_back();
	fileName.erase(fileName.begin());
	OAJ::Compiler::Helpers::WorkingPathManipulator::holdPath(OAJ::Compiler::Helpers::WorkingPathManipulator::retrievePath() / std::filesystem::path{ fileName });
	auto f = std::wofstream{ fileName + OAJ::Compiler::Helpers::StringConverter::to_wstring(extension), std::ios::out };
	f << program;
	f.close();
	return fileName;
}

auto OAJ::Compiler::Compiler::removeTempFile(const std::wstring& tempFileName) -> bool
{
	return std::filesystem::remove(tempFileName);
}

auto OAJ::Compiler::Compiler::compileAndGetOutput(const std::wstring& fileName, const std::string& program, const std::string& extension) -> std::pair<std::string, std::string>
{
	// Set arguments to relative: "folder\folder.cpp" instead of using WorkingPathManipulator
	// std::filesystem::create_directory(path);
	//
	auto arguments = OAJ::Compiler::Helpers::StringConverter::from_wstring(fileName) + extension;
	return OAJ::Compiler::Helpers::ExecStreamWrapper::execute(program, arguments);
}

auto OAJ::Compiler::Compiler::runAndGetOutput(const std::wstring& fileName, const std::string& callableExtension) -> std::pair<std::string, std::string>
{
	auto callableName = OAJ::Compiler::Helpers::StringConverter::from_wstring(fileName) + callableExtension;
	auto outputAndErrors = OAJ::Compiler::Helpers::ExecStreamWrapper::execute(callableName, std::string{});
	OAJ::Compiler::Helpers::WorkingPathManipulator::releasePath();
	return outputAndErrors;
}