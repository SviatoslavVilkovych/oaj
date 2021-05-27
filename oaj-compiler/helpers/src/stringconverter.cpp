#include "helpers/stringconverter.h"

#include <codecvt>

auto OAJ::Helpers::StringConverter::to_wstring(std::string str) -> std::wstring
{
	auto converter = std::wstring_convert<std::codecvt_utf8_utf16<wchar_t>>{};
	return converter.from_bytes(str);
}

auto OAJ::Helpers::StringConverter::to_wstring(GUID guid) -> std::wstring
{
	OLECHAR fileName[MAX_PATH] = {};
	StringFromGUID2(guid, fileName, MAX_PATH);
	return std::wstring{ fileName };
}