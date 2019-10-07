using Iql.Entities.PropertyGroups.Files;
using IqlSampleApp.Data.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;

namespace Iql.Tests.Tests
{
    [TestClass]
    public class FileStateTests : TestsBase
    {
        [TestMethod]
        public void TestGetEmptyFileState()
        {
            var person = new Person();
            person.PhotoState = "";
            var personPhoto = Db.EntityConfigurationContext.EntityType<Person>().Files[0];
            var state = personPhoto.TryGetFileState(person);
            Assert.IsNull(state);
        }

        [TestMethod]
        public void TestGetNullFileState()
        {
            var person = new Person();
            var personPhoto = Db.EntityConfigurationContext.EntityType<Person>().Files[0];
            var state = personPhoto.TryGetFileState(person);
            Assert.IsNull(state);
        }

        [TestMethod]
        public void TestGetBrokenFileState()
        {
            var person = new Person();
            person.PhotoState = "ih1";
            var personPhoto = Db.EntityConfigurationContext.EntityType<Person>().Files[0];
            var state = personPhoto.TryGetFileState(person);
            Assert.IsNull(state);
        }

        [TestMethod]
        public void TestGetValidFileState()
        {
            var person = new Person();
            var sourceState = new FileState
            {
                ContentType = "image",
                IsUploading = true,
                UploadedByUserId = "abc123",
                UploadedFromDeviceId = "def456",
                Url = "test url"
            };
            person.PhotoState = JsonConvert.SerializeObject(sourceState);
            var personPhoto = Db.EntityConfigurationContext.EntityType<Person>().Files[0];
            var state = personPhoto.TryGetFileState(person);
            Assert.IsNotNull(state);
            Assert.AreEqual(sourceState.ContentType, state.ContentType);
            Assert.AreEqual(sourceState.IsUploading, state.IsUploading);
            Assert.AreEqual(sourceState.UploadedByUserId, state.UploadedByUserId);
            Assert.AreEqual(sourceState.UploadedFromDeviceId, state.UploadedFromDeviceId);
            Assert.AreEqual(sourceState.Url, state.Url);
        }

        [TestMethod]
        public void TestSetNullFileState()
        {
            var person = new Person();
            var sourceState = new FileState
            {
                ContentType = "image",
                IsUploading = true,
                UploadedByUserId = "abc123",
                UploadedFromDeviceId = "def456",
                Url = "test url"
            };
            person.PhotoState = JsonConvert.SerializeObject(sourceState);
            var personPhoto = Db.EntityConfigurationContext.EntityType<Person>().Files[0];
            var success = personPhoto.TrySetFileState(person, null);
            Assert.IsTrue(success);
            var state = personPhoto.TryGetFileState(person);
            Assert.IsNull(state);
        }

        [TestMethod]
        public void TestSetValidFileState()
        {
            var person = new Person();
            var sourceState = new FileState
            {
                ContentType = "image",
                IsUploading = true,
                UploadedByUserId = "abc123",
                UploadedFromDeviceId = "def456",
                Url = "test url"
            };
            person.PhotoState = JsonConvert.SerializeObject(sourceState);
            var personPhoto = Db.EntityConfigurationContext.EntityType<Person>().Files[0];
            var success = personPhoto.TrySetFileState(person, sourceState);
            Assert.IsTrue(success);
            var state = personPhoto.TryGetFileState(person);
            Assert.IsNotNull(state);
            Assert.AreEqual(sourceState.ContentType, state.ContentType);
            Assert.AreEqual(sourceState.IsUploading, state.IsUploading);
            Assert.AreEqual(sourceState.UploadedByUserId, state.UploadedByUserId);
            Assert.AreEqual(sourceState.UploadedFromDeviceId, state.UploadedFromDeviceId);
            Assert.AreEqual(sourceState.Url, state.Url);
        }
    }
}
