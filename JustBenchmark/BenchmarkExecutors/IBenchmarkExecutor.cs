using System.Reflection;

namespace JustBenchmark.BenchmarkExecutors {
	/// <summary>
	/// Interface for benchmark executor
	/// </summary>
	public interface IBenchmarkExecutor {
		/// <summary>
		/// Execute method for benchmark and return result
		/// </summary>
		/// <param name="method">Method information</param>
		/// <param name="instance">Instance object</param>
		/// <returns></returns>
		BenchmarkResult Execute(MethodInfo method, object instance);
	}
}
