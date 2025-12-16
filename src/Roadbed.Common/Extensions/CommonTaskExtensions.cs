/*
 * The namespace Roadbed.Common.Extensions was removed on purpose and replaced with Roadbed so that no additional using statements are required.
 */
namespace Roadbed
{
    using System;
    using System.Threading.Tasks;

    /// <summary>
    /// Extensions for common Task operations.
    /// </summary>
    public static class CommonTaskExtensions
    {
        #region Public Methods

        /// <summary>
        /// Wraps a Task with a timeout.
        /// For more context, see https://stackoverflow.com/questions/4238345/asynchronously-wait-for-taskt-to-complete-with-timeout.
        /// </summary>
        /// <typeparam name="TResult">Object type to be returned within the Task.</typeparam>
        /// <param name="task">Task to append with with a Timeout.</param>
        /// <param name="timeout">Amount of time to wait before cancelling the Task.</param>
        /// <returns>Completed Task.</returns>
        /// <exception cref="TimeoutException">Invoked when completed task doesn't match the original task.</exception>
        /// <remarks>
        /// <code>
        /// var response = await client.SendAsync(message).TimeoutAfter(TimeSpan.FromSeconds(30));
        /// </code>
        /// </remarks>
        public static async Task<TResult> TimeoutAfter<TResult>(this Task<TResult> task, TimeSpan timeout)
        {
            using (var timeoutCancellationTokenSource = new CancellationTokenSource())
            {
                var completedTask = await Task.WhenAny(task, Task.Delay(timeout, timeoutCancellationTokenSource.Token));

                if (completedTask == task)
                {
                    await timeoutCancellationTokenSource.CancelAsync();

                    // Very important in order to propagate exceptions
                    return await task;
                }
                else
                {
                    throw new TimeoutException("The operation has timed out.");
                }
            }
        }

        /// <summary>
        /// Wraps a Task with a timeout.
        /// For more context, see https://stackoverflow.com/questions/4238345/asynchronously-wait-for-taskt-to-complete-with-timeout.
        /// </summary>
        /// <typeparam name="TResult">Object type to be returned within the Task.</typeparam>
        /// <param name="task">Task to append with with a Timeout.</param>
        /// <param name="timeoutInSeconds">Amount of time to wait before cancelling the Task.</param>
        /// <returns>Completed Task.</returns>
        /// <exception cref="TimeoutException">Invoked when completed task doesn't match the original task.</exception>
        /// <remarks>
        /// <code>
        /// var response = await client.SendAsync(message).TimeoutAfter(30);
        /// </code>
        /// </remarks>
        public static async Task<TResult> TimeoutAfter<TResult>(this Task<TResult> task, int timeoutInSeconds)
        {
            return await task.TimeoutAfter(TimeSpan.FromSeconds(timeoutInSeconds));
        }

        #endregion Public Methods
    }
}
