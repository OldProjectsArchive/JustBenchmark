using JustBenchmark.BenchmarkExecutors;
using System;
using System.Reflection;

namespace JustBenchmark {
	/// <summary>
	/// Simple single thread benchmark for blocking method
	/// </summary>
	public class BenchmarkAttribute : Attribute, IBenchmarkExecutor {
		/// <summary>
		/// Iteration count
		/// </summary>
		public int Iteration { get; set; } = 10000;

		/// <summary>
		/// Initialize
		/// </summary>
		public BenchmarkAttribute() {
		}

		/// <summary>
		/// Initialize
		/// </summary>
		/// <param name="iteration"></param>
		public BenchmarkAttribute(int iteration) {
			Iteration = iteration;
		}

		/// <summary>
		/// Execute benchmark
		/// </summary>
		public BenchmarkResult Execute(MethodInfo method, object instance) {
			var action = (Action)method.CreateDelegate(typeof(Action), instance);
			var result = this.GenericExecute(method, () => {
				for (int from = 0, to = Iteration; from < to; ++from) {
					action();
				}
			});
			return result;
		}
	}
}
