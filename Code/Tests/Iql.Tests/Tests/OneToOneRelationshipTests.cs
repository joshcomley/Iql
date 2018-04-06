using System;
using System.Linq;
using System.Threading.Tasks;
using Iql.Tests.Context;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Tunnel.App.Data.Entities;

namespace Iql.Tests.Tests
{
    [TestClass]
    public class OneToOneRelationshipTests : TestsBase
    {
        [TestMethod]
        public void OneToOneRelationshipsSetShouldPersistRelationshipsWhenTrackedAfterAdd()
        {
            Perform(OneToOneTestType.Reference, OneToOneTestOrder.AfterAdd);
            //Perform(OneToOneTestType.Key, OneToOneTestOrder.BeforeAdd);
            //Perform(OneToOneTestType.Key, OneToOneTestOrder.AfterAdd);
        }

        [TestMethod]
        public void OneToOneRelationshipsSetShouldPersistRelationshipsWhenTrackedBeforeAdd()
        {
            Perform(OneToOneTestType.Reference, OneToOneTestOrder.BeforeAdd);
        }

        void Perform(OneToOneTestType oneToOneTestType,
            OneToOneTestOrder order)
        {
            var count = 4;
            for (var i = 0; i < count; i++)
            {
                OneToOneShouldPersistRelationshipsWhenTracked(false, order, oneToOneTestType);
                TestCleanUp();
            }
            for (var i = 0; i < count; i++)
            {
                OneToOneShouldPersistRelationshipsWhenTracked(true, order, oneToOneTestType);
                TestCleanUp();
            }
        }

        [TestMethod]
        public async Task DeletingSourceOfOneToOneShouldPersistRelationshipDeletionsWhenTracked()
        {
            AppDbContext.InMemoryDb.SiteInspections.Add(new SiteInspection { Id = 62 });
            AppDbContext.InMemoryDb.SiteInspections.Add(new SiteInspection { Id = 63 });
            AppDbContext.InMemoryDb.RiskAssessments.Add(new RiskAssessment { Id = 3, SiteInspectionId = 62 });
            AppDbContext.InMemoryDb.RiskAssessments.Add(new RiskAssessment { Id = 4, SiteInspectionId = 63 });
            var siteInspections = await Db.SiteInspections.Expand(s => s.RiskAssessment).ToListAsync();
            Db.DeleteEntity(siteInspections[0].RiskAssessment
#if TypeScript
                , typeof(RiskAssessment)
#endif
                );
            Assert.IsNull(siteInspections[0].RiskAssessment);
            Assert.IsNotNull(siteInspections[1].RiskAssessment);
        }

        [TestMethod]
        public async Task DeletingTargetOfOneToOneShouldCascadeRelationshipDeletions()
        {
            AppDbContext.InMemoryDb.SiteInspections.Add(new SiteInspection { Id = 62 });
            AppDbContext.InMemoryDb.SiteInspections.Add(new SiteInspection { Id = 63 });
            AppDbContext.InMemoryDb.RiskAssessments.Add(new RiskAssessment { Id = 3, SiteInspectionId = 62 });
            AppDbContext.InMemoryDb.RiskAssessments.Add(new RiskAssessment { Id = 4, SiteInspectionId = 63 });
            AppDbContext.InMemoryDb.RiskAssessmentSolutions.Add(new RiskAssessmentSolution { Id = 26, RiskAssessmentId = 3 });
            AppDbContext.InMemoryDb.RiskAssessmentSolutions.Add(new RiskAssessmentSolution { Id = 27, RiskAssessmentId = 4 });
            var siteInspections = await Db.SiteInspections.ExpandSingle(s => s.RiskAssessment, q => q.Expand(r => r.RiskAssessmentSolution)).ToListAsync();
            var riskAssessment = siteInspections[0].RiskAssessment;
            var tracking = Db.DataStore.Tracking;
            //Assert.IsTrue(tracking.IsTracked(riskAssessment, typeof(RiskAssessment)));
            var solution = riskAssessment.RiskAssessmentSolution;
            //Assert.IsTrue(tracking.IsTracked(solution, typeof(RiskAssessmentSolution)));
            var riskAssessmentState = tracking.TrackingSetByType(typeof(RiskAssessment)).GetEntityState(riskAssessment);
            var riskAssessmentSolutionState = tracking.TrackingSetByType(typeof(RiskAssessmentSolution)).GetEntityState(solution);

            Assert.IsFalse(riskAssessmentState.MarkedForDeletion);
            Assert.IsFalse(riskAssessmentState.MarkedForCascadeDeletion);
            Assert.IsFalse(riskAssessmentState.CascadeDeletedBy.Any(cd => cd.Source == siteInspections[0]));
            Assert.IsFalse(riskAssessmentSolutionState.MarkedForDeletion);
            Assert.IsFalse(riskAssessmentSolutionState.MarkedForCascadeDeletion);
            Assert.IsFalse(riskAssessmentSolutionState.CascadeDeletedBy.Any(cd => cd.Source == riskAssessment));

            Db.DeleteEntity(siteInspections[0]  
#if TypeScript
            , typeof(SiteInspection)
#endif
                );

            Assert.IsFalse(riskAssessmentState.MarkedForDeletion);
            Assert.IsTrue(riskAssessmentState.MarkedForCascadeDeletion);
            Assert.IsTrue(riskAssessmentState.CascadeDeletedBy.Count(cd => cd.Source == siteInspections[0]) == 1);
            Assert.IsFalse(riskAssessmentSolutionState.MarkedForDeletion);
            Assert.IsTrue(riskAssessmentSolutionState.MarkedForCascadeDeletion);
            Assert.IsTrue(riskAssessmentSolutionState.CascadeDeletedBy.Count(cd => cd.Source == riskAssessment) == 1);
        }

        [TestMethod]
        public async Task DeletingTargetOfOneToOneShouldCascadeRelationshipDeletions2()
        {
            AppDbContext.InMemoryDb.People.Add(new Person { Id = 62, TypeId = 52});
            AppDbContext.InMemoryDb.People.Add(new Person { Id = 63 });
            AppDbContext.InMemoryDb.PeopleTypes.Add(new PersonType { Id = 53 });
            AppDbContext.InMemoryDb.PeopleTypes.Add(new PersonType { Id = 52 });
            AppDbContext.InMemoryDb.PeopleTypeMap.Add(new PersonTypeMap { PersonId = 62, TypeId =  53});
            AppDbContext.InMemoryDb.PeopleTypeMap.Add(new PersonTypeMap { PersonId = 63, TypeId =  52});

            var people = await Db.People.ExpandCollection(s => s.Types, q => q.Expand(r => r.Type)).ToListAsync();
            Assert.IsNotNull(people[0].Types[0].Type);
            Assert.AreEqual(people.Single(p => p.TypeId == 52), people.Single(p => p.Types[0].TypeId == 52).Types[0].Type.People[0]);

            var personTypeMap = await Db.PersonTypesMap.Where(p => p.PersonId == 62 && p.TypeId == 53).SingleAsync();
            var person = people.Single(p => p.Id == 62);
            Db.People.Delete(person);
            Assert.IsTrue(Db.DataStore.Tracking.IsMarkedForCascadeDeletion(personTypeMap, typeof(PersonTypeMap)));
            Assert.AreEqual(1, person.Types.Count);
        }

        [TestMethod]
        public async Task OneToOneShouldPersistRelationshipChangesWhenTracked()
        {
            // A one to one relationship has been changed at the database end
            // so when retrieve the latest version it should update accordingly
            var riskAssessment1 = new RiskAssessment();
            var riskAssessment2 = new RiskAssessment();
            //riskAssessment1.Id = 7;
            //riskAssessment2.Id = 8;
            riskAssessment1.SiteInspectionId = 62;
            riskAssessment2.SiteInspectionId = 72;
            Db.RiskAssessments.Add(riskAssessment1);
            Db.RiskAssessments.Add(riskAssessment2);
            await Db.SaveChangesAsync();
            var queuedOperations = Db.DataStore.GetQueue().ToList();
            Assert.AreEqual(0, queuedOperations.Count);
            Assert.IsNull(riskAssessment1.SiteInspection);
            Assert.IsNull(riskAssessment2.SiteInspection);
            var changes = Db.DataStore.GetChanges().ToList();
            Assert.AreEqual(0, changes.Count);

            // Externally modify the database
            var dbRiskAssessment = AppDbContext.InMemoryDb.RiskAssessments.Single(r => r.SiteInspectionId == 62);
            dbRiskAssessment.SiteInspectionId = 61;
            AppDbContext.InMemoryDb.RiskAssessments.Add(
                new RiskAssessment
                {
                    Id = 9,
                    SiteInspectionId = 62
                });
            AppDbContext.InMemoryDb.SiteInspections.Add(
                new SiteInspection
                {
                    Id = 62
                });

            changes = Db.DataStore.GetChanges().ToList();
            Assert.AreEqual(0, changes.Count);
            var siteInspection = await Db.SiteInspections.Expand(d => d.RiskAssessment).WithKeyAsync(62);
            changes = Db.DataStore.GetChanges().ToList();
            Assert.AreEqual(0, changes.Count);
            queuedOperations = Db.DataStore.GetQueue().ToList();
            Assert.AreEqual(0, queuedOperations.Count);
            Assert.AreEqual(siteInspection.RiskAssessment.Id, 9);
            Assert.AreEqual(siteInspection.RiskAssessment.SiteInspectionId, siteInspection.Id);
            Assert.AreEqual(siteInspection.RiskAssessment.SiteInspection, siteInspection);
            Assert.AreEqual(62, riskAssessment1.SiteInspectionId);
        }
        public enum OneToOneTestType
        {
            Reference,
            Key
        }
        public enum OneToOneTestOrder
        {
            BeforeAdd,
            AfterAdd
        }
        public void OneToOneShouldPersistRelationshipsWhenTracked(
            bool reverseAddOrder,
            OneToOneTestOrder order,
            OneToOneTestType type)
        {
            var riskAssessment1 = new RiskAssessment();
            var riskAssessment2 = new RiskAssessment();
            var siteInspection1 = new SiteInspection();
            var siteInspection2 = new SiteInspection();

            void SetRelationships()
            {
                switch (type)
                {
                    case OneToOneTestType.Reference:
                        riskAssessment1.SiteInspection = siteInspection1;
                        riskAssessment2.SiteInspection = siteInspection2;
                        break;
                    case OneToOneTestType.Key:
                        riskAssessment1.SiteInspectionId = siteInspection1.Id;
                        riskAssessment2.SiteInspectionId = siteInspection2.Id;
                        break;
                }
            }

            if (order == OneToOneTestOrder.BeforeAdd)
            {
                SetRelationships();
            }
            void AddInspections()
            {
                Db.SiteInspections.Add(siteInspection1);
                Db.SiteInspections.Add(siteInspection2);
            }

            if (!reverseAddOrder)
            {
                AddInspections();
            }
            Db.RiskAssessments.Add(riskAssessment1);
            Db.RiskAssessments.Add(riskAssessment2);
            if (reverseAddOrder)
            {
                AddInspections();
            }
            if (order == OneToOneTestOrder.AfterAdd)
            {
                SetRelationships();
            }
            Assert.AreEqual(siteInspection1, riskAssessment1.SiteInspection);
            Assert.AreEqual(siteInspection1.Id, riskAssessment1.SiteInspectionId);
            Assert.AreEqual(siteInspection2, riskAssessment2.SiteInspection);
            Assert.AreEqual(siteInspection2.Id, riskAssessment2.SiteInspectionId);
            Assert.AreEqual(siteInspection1.RiskAssessment, riskAssessment1);
            Assert.AreEqual(siteInspection2.RiskAssessment, riskAssessment2);
        }

        [TestMethod]
        public async Task RelationshipsShouldBeMatchedWhenSourceIsLoadedAfterTarget()
        {
            AppDbContext.InMemoryDb.SiteInspections.Add(new SiteInspection{Id = 17});
            AppDbContext.InMemoryDb.SiteInspections.Add(new SiteInspection{Id = 18 });
            AppDbContext.InMemoryDb.RiskAssessments.Add(new RiskAssessment { Id = 28, SiteInspectionId = 17 });
            AppDbContext.InMemoryDb.RiskAssessments.Add(new RiskAssessment { Id = 29, SiteInspectionId = 18 });
            AppDbContext.InMemoryDb.RiskAssessmentSolutions.Add(new RiskAssessmentSolution { Id = 39, RiskAssessmentId = 28 });
            AppDbContext.InMemoryDb.RiskAssessmentSolutions.Add(new RiskAssessmentSolution { Id = 40, RiskAssessmentId = 29 });

            var siteInspections = await Db.SiteInspections.ToListAsync();
            foreach (var inspection in siteInspections)
            {
                Assert.IsNull(inspection.RiskAssessment);
            }

            var riskAssessments = await Db.RiskAssessments.ToListAsync();
            foreach (var inspection in siteInspections)
            {
                Assert.AreEqual(inspection.RiskAssessment.SiteInspectionId, inspection.RiskAssessment.SiteInspection.Id);
                Assert.AreEqual(inspection.RiskAssessment.SiteInspectionId, inspection.Id);
            }

            foreach (var riskAssessment in riskAssessments)
            {
                Assert.IsNull(riskAssessment.RiskAssessmentSolution);
            }

            var riskAssessmentSolutions = await Db.RiskAssessmentSolutions.ToListAsync();
            foreach (var riskAssessment in riskAssessments)
            {
                Assert.IsNotNull(riskAssessment.RiskAssessmentSolution);
                Assert.AreEqual(riskAssessment.Id, riskAssessment.RiskAssessmentSolution.RiskAssessmentId);
            }

            foreach (var riskAssessmentSolution in riskAssessmentSolutions)
            {
                Assert.AreEqual(riskAssessmentSolution.RiskAssessmentId, riskAssessmentSolution.RiskAssessment.Id);
            }
        }
    }
}