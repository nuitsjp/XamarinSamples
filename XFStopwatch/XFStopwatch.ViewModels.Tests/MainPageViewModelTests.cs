using System;
using System.Collections.ObjectModel;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using XFStopwatch.Models;

namespace XFStopwatch.ViewModels.Tests
{
    [TestClass]
    public class MainPageViewModelTests
    {
        [TestMethod]
        public void MainPageViewModelTest()
        {
            var stopwatch = new Mock<IStopwatch>();

            var elapsedTime = TimeSpan.FromSeconds(100);
            stopwatch.Setup(m => m.ElapsedTime).Returns(elapsedTime);

            var status = StopwatchStatus.Paused;
            stopwatch.Setup(m => m.Status).Returns(status);

            var lapTime = new LapTime(1, TimeSpan.MaxValue);
            var lapTimes = new ReadOnlyObservableCollection<LapTime>(new ObservableCollection<LapTime> { lapTime });
            stopwatch.Setup(m => m.LapTimes).Returns(lapTimes);

            var viewModel = new MainPageViewModel(stopwatch.Object);


            Assert.AreEqual(elapsedTime, viewModel.ElapsedTime);
            Assert.AreEqual(status, viewModel.Status);
            Assert.IsNotNull(viewModel.LapTimes);
            Assert.AreEqual(1, viewModel.LapTimes.Count);
            Assert.AreEqual(lapTime, viewModel.LapTimes[0]);

            Assert.IsNotNull(viewModel.StartOrStopCommand);
            Assert.IsTrue(viewModel.StartOrStopCommand.CanExecute(null));
            viewModel.StartOrStopCommand.Execute(null);
            stopwatch.Verify(m => m.Start());

            Assert.IsNotNull(viewModel.PauseOrResetCommand);
            Assert.IsFalse(viewModel.PauseOrResetCommand.CanExecute(null));

        }
    }
}
