#include "helpers/execstreamwrapper.h"

#include "libexecstream/exec-stream.h"

auto OAJ::Compiler::Helpers::ExecStreamWrapper::execute(const std::string& program, const std::string& arguments)->std::pair<std::string, std::string>
{
	auto output = std::string{}, errors = std::string{};
	try {
		auto es = exec_stream_t{};
		es.start(program, arguments);
		auto temp_output = std::string{}, temp_errors = std::string{};
		while (std::getline(es.out(), temp_output).good()) {
			output += ('\n' + temp_output);
		}
		while (std::getline(es.err(), temp_errors).good()) {
			errors += ('\n' + temp_errors);
		}
	}
	catch (std::exception const& e) {
		auto error = "error: " + std::string{ e.what() };
	}
	return std::pair{ output, errors };
}