#pragma once

#include <string>
#include <combaseapi.h>

namespace OAJ::Helpers
{
	class StringConverter
	{
	public:
		static auto to_wstring(std::string str)->std::wstring;
		static auto to_wstring(GUID str)->std::wstring;
	};
}
