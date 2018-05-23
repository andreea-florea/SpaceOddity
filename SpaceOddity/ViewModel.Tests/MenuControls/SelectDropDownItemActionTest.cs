using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ViewModel.MenuControls;
using Moq;

namespace ViewModel.Tests.MenuControls
{
    [TestClass]
    public class SelectDropDownItemActionTest
    {
        [TestMethod]
        public void CanSelectDropDownItem()
        {
            var mockDropDownList = new Mock<IDropDownList>();
            var action = new SelectDropDownItemAction(mockDropDownList.Object, 3);
            action.Execute();
            mockDropDownList.VerifySet(dropDown => dropDown.SelectedIndex = 3);
            mockDropDownList.Verify(dropDown => dropDown.Close(), Times.Once());
        }
    }
}
