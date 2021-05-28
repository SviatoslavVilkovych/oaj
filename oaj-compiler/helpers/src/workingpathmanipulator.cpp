#include "helpers/workingpathmanipulator.h"

#include <filesystem>

bool OAJ::Compiler::Helpers::WorkingPathManipulator::isOperatingWith = bool{ false };
std::wstring OAJ::Compiler::Helpers::WorkingPathManipulator::m_defaultPath = std::filesystem::current_path();
std::wstring OAJ::Compiler::Helpers::WorkingPathManipulator::m_path = std::filesystem::current_path();

auto OAJ::Compiler::Helpers::WorkingPathManipulator::holdPath(const std::wstring& path) -> bool
{
	std::filesystem::current_path();
	std::filesystem::create_directory(path);
	std::filesystem::current_path(path);
	if (!isOperatingWith)
	{
		isOperatingWith = true;
		std::filesystem::current_path(path);
		m_path = path;
		return true;
	}
	return false;
}

auto OAJ::Compiler::Helpers::WorkingPathManipulator::releasePath() -> bool
{
	std::filesystem::current_path(m_defaultPath);
	m_path = {};
	isOperatingWith = false;
	return true;
}

auto OAJ::Compiler::Helpers::WorkingPathManipulator::retrievePath() -> std::wstring
{
	return m_path;
}