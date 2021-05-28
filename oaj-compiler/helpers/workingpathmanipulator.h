#pragma once

#include <string>

namespace OAJ::Compiler::Helpers
{
	class WorkingPathManipulator
	{
		static std::wstring m_defaultPath;
		static std::wstring m_path;
		static bool isOperatingWith;
	public:
		static auto holdPath(const std::wstring& path) -> bool;
		static auto retrievePath() -> std::wstring;
		static auto releasePath() -> bool;
	};
}
