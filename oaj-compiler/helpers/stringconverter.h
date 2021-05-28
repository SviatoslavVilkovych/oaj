#pragma once

#include <combaseapi.h>
#include <codecvt>
#include <string>

namespace OAJ::Compiler::Helpers
{
	class StringConverter
	{
	private:
		inline static std::wstring_convert<std::codecvt_utf8_utf16<wchar_t>> m_converter;
	public:
		static auto to_wstring(const std::string& str) -> std::wstring;
		static auto from_wstring(const std::wstring& str) -> std::string;
		static auto to_wstring(GUID str)->std::wstring;
	};
}
