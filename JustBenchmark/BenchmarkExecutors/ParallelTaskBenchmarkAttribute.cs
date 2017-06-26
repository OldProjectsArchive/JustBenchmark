using JustBenchmark.BenchmarkExecutors;
using System;
using System.Reflection;
using System.Threading.Tasks;

namespace JustBenchmark {
	/// <summary>
	/// Parallel line benchmark for method returns Task
	/// </summary>
	public class ParallelTaskBenchmarkAttribute : Attribute, IBenchmarkExecutor {
		/// <summary>
		/// Iteration count
		/// </summary>
		public int Iteration { get; set; } = 10000;
		/// <summary>
		/// Parallel degree
		/// </summary>
		public int Degree { get; set; } = Environment.ProcessorCount;

		/// <summary>
		/// Initialize
		/// </summary>
		public ParallelTaskBenchmarkAttribute() {

		}

		/// <summary>
		/// Initialize
		/// </summary>
		/// <param name="iteration"></param>
		public ParallelTaskBenchmarkAttribute(int iteration) {
			Iteration = iteration;
		}

		/// <summary>
		/// Initialize
		/// </summary>
		public ParallelTaskBenchmarkAttribute(int iteration, int degree) {
			Iteration = iteration;
			Degree = degree;
		}

		/// <summary>
		/// Execute benchmark
		/// </summary>
		public BenchmarkResult Execute(MethodInfo method, object instance) {
			var func = (Func<Task>)method.CreateDelegate(typeof(Func<Task>), instance);
			var initialTaskCount = Math.Min(Degree, Iteration);
			var totalTaskCount = Iteration - initialTaskCount;
			var taskArray = new Task[initialTaskCount];
			var result = this.GenericExecute(method, () => {
				for (int from = 0, to = taskArray.Length; from < to; ++from) {
					taskArray[from] = func();
				}
				while (totalTaskCount > 0) {
					var index = Task.WaitAny(taskArray);
					taskArray[index] = func();
					--totalTaskCount;
				}
				Task.WaitAll(taskArray);
			});
			return result;
		}
	}
}
