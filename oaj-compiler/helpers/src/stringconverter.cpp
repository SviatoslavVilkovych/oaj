#include "helpers/stringconverter.h"

auto OAJ::Compiler::Helpers::StringConverter::to_wstring(const std::string& str) -> std::wstring
{
	return m_converter.from_bytes(str);
}

auto OAJ::Compiler::Helpers::StringConverter::from_wstring(const std::wstring& str) -> std::string
{
	return m_converter.to_bytes(str);
}

auto OAJ::Compiler::Helpers::StringConverter::to_wstring(GUID guid) -> std::wstring
{
	OLECHAR fileName[MAX_PATH] = {};
	StringFromGUID2(guid, fileName, MAX_PATH);
	return std::wstring{ fileName };
}