using JustBenchmark.BenchmarkExecutors;
using System;
using System.Reflection;
using System.Threading.Tasks;

namespace JustBenchmark {
	/// <summary>
	/// Simple line benchmark for method returns Task
	/// </summary>
	public class TaskBenchmarkAttribute : Attribute, IBenchmarkExecutor {
		/// <summary>
		/// Iteration count
		/// </summary>
		public int Iteration { get; set; } = 10000;

		/// <summary>
		/// Initialize
		/// </summary>
		public TaskBenchmarkAttribute() {
		}

		/// <summary>
		/// Initialize
		/// </summary>
		/// <param name="iteration"></param>
		public TaskBenchmarkAttribute(int iteration) {
			Iteration = iteration;
		}

		/// <summary>
		/// Execute benchmark
		/// </summary>
		public BenchmarkResult Execute(MethodInfo method, object instance) {
			var func = (Func<Task>)method.CreateDelegate(typeof(Func<Task>), instance);
			var benchmarkTask = new Func<Task>(async () => {
				for (int from = 0, to = Iteration; from < to; ++from) {
					await func();
				}
			});
			var result = this.GenericExecute(method, () => {
				benchmarkTask().Wait();
			});
			return result;
		}
	}
}
