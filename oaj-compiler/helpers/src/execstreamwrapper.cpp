#include "helpers/execstreamwrapper.h"

#include "libexecstream/exec-stream.h"

auto OAJ::Compiler::Helpers::ExecStreamWrapper::execute(const std::string& program, const std::string& arguments)->std::pair<std::string, std::string>
{
	auto output = std::string{}, errors = std::string{};

	auto es = exec_stream_t{};
	es.start(program, arguments);
	auto temp_output = std::string{}, temp_errors = std::string{};
	while (std::getline(es.out(), temp_output).good()) {
		output += (temp_output + '\n');
	}
	while (std::getline(es.err(), temp_errors).good()) {
		errors += (temp_errors + '\n');
	}

	output += temp_output;
	errors += temp_errors;

	es.close();
	return std::pair{ output, errors };
}