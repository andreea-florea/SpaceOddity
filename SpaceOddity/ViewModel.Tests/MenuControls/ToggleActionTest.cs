using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ViewModel.MenuControls;
using Moq;

namespace ViewModel.Tests.MenuControls
{
    [TestClass]
    public class ToggleActionTest
    {
        [TestMethod]
        public void ActionCanToggleDropDownList()
        {
            var mockDropDownList = new Mock<IDropDownList>();
            var action = new ToggleAction(mockDropDownList.Object);
            action.Execute();
            mockDropDownList.Verify(dropDownList => dropDownList.Toggle(), Times.Once());
        }
    }
}
