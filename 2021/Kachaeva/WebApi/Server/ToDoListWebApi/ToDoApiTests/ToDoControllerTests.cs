﻿using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ToDoApiDependencies;
using ToDoApi.Controllers;
using System.Threading.Tasks;

namespace ToDoApiTests
{
    [TestClass]
    public class ToDoControllerTests
    {
        [TestMethod]
        public void GetWorksCorrectlyWithEmptyList()
        {
            var loaderSaver = new FakeLoaderAndSaver();
            var controller = new ToDoController(loaderSaver);

            var actualToDoList = controller.GetToDoList();

            Assert.AreEqual(0, actualToDoList.Count());
        }

        [TestMethod]
        public void GetReturnsCorrectList()
        {
            var loaderSaver = new FakeLoaderAndSaver();
            loaderSaver.ToDoList.Add(new ToDoTask("wash dishes", false));
            var controller = new ToDoController(loaderSaver);

            var actualToDoList = controller.GetToDoList();

            Assert.AreEqual(1, actualToDoList.Count());
            Assert.AreEqual("1. wash dishes  [ ]", actualToDoList.First().ToString());
        }

        [TestMethod]
        public void TaskExistsAfterPost()
        {
            var loaderSaver = new FakeLoaderAndSaver();
            var controller = new ToDoController(loaderSaver);

            controller.PostTask(new ToDoTask("wash dishes", false));
            var actualToDoList = controller.GetToDoList();

            Assert.AreEqual(1, actualToDoList.Count());
            Assert.AreEqual("1. wash dishes  [ ]", actualToDoList.First().ToString());
;
        }

        [TestMethod]
        public void TaskDoesNotExistAfterDelete()
        {
            var loaderSaver = new FakeLoaderAndSaver();
            loaderSaver.ToDoList.Add(new ToDoTask("wash dishes", false));
            var controller = new ToDoController(loaderSaver);

            controller.DeleteTask(1);
            var actualToDoList = controller.GetToDoList();

            Assert.AreEqual(0, actualToDoList.Count());
        }

        [TestMethod]
        public void TaskTextChangesAfterPatch()
        {
            var loaderSaver = new FakeLoaderAndSaver();
            loaderSaver.ToDoList.Add(new ToDoTask("wash dishes" , false));
            var controller = new ToDoController(loaderSaver);

            controller.PatchTask(1, new PatchToDoTask{Text="clean the room"});
            var actualToDoList = controller.GetToDoList();

            Assert.AreEqual(1, actualToDoList.Count());
            Assert.AreEqual("1. clean the room  [ ]", actualToDoList.First().ToString());
        }

        [TestMethod]
        public void TaskStatusTogglesAfterPatch()
        {
            var loaderSaver = new FakeLoaderAndSaver();
            loaderSaver.ToDoList.Add(new ToDoTask("wash dishes", false));
            var controller = new ToDoController(loaderSaver);

            controller.PatchTask(1, new PatchToDoTask{IsDone = true});
            var actualToDoList = controller.GetToDoList();

            Assert.AreEqual(1, actualToDoList.Count());
            Assert.AreEqual("1. wash dishes  [v]", actualToDoList.First().ToString());
        }
    }
}