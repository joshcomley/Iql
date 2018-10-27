﻿using System.Collections.Generic;

namespace Iql.Tests.Data.Context
{
    public class ODataFakeHttpRequestResults
    {
        static ODataFakeHttpRequestResults()
        {
            var requestResults = new Dictionary<string, string>();
            //http://localhost:58000/odata/Users('2b2b0e44-4579-4965-8e3a-097e6684b767')$count=true&$expand=ExamResults($expand=Exam,Video,Results($expand=Hazard))
            requestResults.Add(@"http://localhost:28000/odata/Sites?$count=true",
                @"{""@odata.context"":""http://localhost:28000/odata/$metadata#Clients"",""value"":[{""Id"":1,""Description"":""Hey"",""Area"":{""type"":""Polygon"",""coordinates"":[[[-80.19,25.774],[-66.118,18.466],[-64.757,32.321],[-80.19,25.774]]]},""Location"":{""type"":""Point"",""coordinates"":[13.2846523,52.5067614]},""Line"":{""type"":""LineString"",""coordinates"":[[-80.19,25.774],[-66.118,18.466],[-64.757,32.321]]}},{""Id"":2,""Description"":""You"",""Area"":null,""Location"":null,""Line"":null}]}");
            requestResults.Add(@"http://localhost:28000/odata/Sites(1)",
                @"{""@odata.context"":""http://localhost:28000/odata/$metadata#Sites/$entity"",""Id"":1,""Description"":""Hey"",""Area"":{""type"":""Polygon"",""coordinates"":[[[-80.19,25.774],[-66.118,18.466],[-64.757,32.321],[-80.19,25.774]]]},""Location"":{""type"":""Point"",""coordinates"":[13.2846523,52.5067614]},""Line"":{""type"":""LineString"",""coordinates"":[[-80.19,25.774],[-66.118,18.466],[-64.757,32.321]]}}");
            requestResults.Add(@"http://localhost:58000/odata/Users('2b2b0e44-4579-4965-8e3a-097e6684b767')?$expand=ExamResults($expand=Exam,Video,Results($expand=Hazard))", 
                @"{""@odata.context"":""http://localhost:58000/odata/$metadata#Users/$entity"",""Id"":""2b2b0e44-4579-4965-8e3a-097e6684b767"",""ClientGuid"":""0902543c-39b5-40e8-84ef-8ebc0c5ce8ef"",""ClientId"":3,""CreatedByUserId"":null,""Email"":""mrice@mountanvil.com"",""UserType"":""Candidate"",""FullName"":""Michelle Rice"",""IsLockedOut"":false,""UserName"":""mrice@mountanvil.com"",""NormalizedUserName"":""MRICE@MOUNTANVIL.COM"",""NormalizedEmail"":""MRICE@MOUNTANVIL.COM"",""EmailConfirmed"":true,""PasswordHash"":""AQAAAAEAACcQAAAAEDoUI4POIMX73w+X4kalY0btpWvUdlfQf1M3ceEYtUG1BfN/a2WUE9RoyNuFw1zTRg=="",""SecurityStamp"":""4133202a-cf5d-40a0-ab4c-c11dbcb0ac2d"",""ConcurrencyStamp"":""314e7509-7175-4d89-be30-ffc46a4fc884"",""PhoneNumber"":null,""PhoneNumberConfirmed"":false,""TwoFactorEnabled"":false,""LockoutEnd"":null,""LockoutEnabled"":true,""AccessFailedCount"":0,""ExamResults"":[{""VideoId"":5,""ExamId"":2,""ClientId"":3,""ExamCandidateId"":2,""CandidateId"":""2b2b0e44-4579-4965-8e3a-097e6684b767"",""Id"":6,""CreatedByUserId"":null,""ClientGuid"":""0902543c-39b5-40e8-84ef-8ebc0c5ce8ef"",""Score"":0.18181818181818185,""Pass"":false,""ClickData"":""{\""$id\"":\""1\"",\""VideoId\"":null,\""Time\"":[16.4728052,23.9148953],\""X\"":[52.10526162819157,33.5588957134046],\""Y\"":[88.378619085706958,85.527835497611889]}"",""ClickCount"":2,""HazardCount"":11,""SuccessCount"":2,""Date"":""2017-04-19T14:58:08.3859375Z"",""Guid"":""00000000-0000-0000-0000-000000000000"",""CreatedDate"":""0001-01-01T00:00:00Z"",""Version"":0,""PersistenceKey"":""00000000-0000-0000-0000-000000000000"",""Exam"":{""Id"":2,""VideoId"":5,""ClientId"":3,""CreatedByUserId"":null,""VideoGuid"":""b8a8e29d-e485-4ce6-b22d-2e1d990f6b84"",""ClientGuid"":""0902543c-39b5-40e8-84ef-8ebc0c5ce8ef"",""IsTraining"":false,""AvailableToAllUsers"":false,""ScheduledDate"":""2017-04-18T10:00:00Z"",""PassMark"":100,""Title"":""Michelle Exam 2"",""AllowAnyArea"":false,""Description"":null,""Status"":""Completed"",""NotStarted"":false,""Complete"":true,""InProgress"":false,""CandidateCount"":1,""ImageUrl"":""http://127.0.0.1:10000/devstoreaccount1/0902543c-39b5-40e8-84ef-8ebc0c5ce8ef/b8a8e29d-e485-4ce6-b22d-2e1d990f6b84-screenshot-mini?sv=2017-04-17&sr=b&si=ReadOnly&sig=08%2Fgx3jC70N48NsW07UIz%2B1SUkYW8jLmxgQ0CdKNfPM%3D&revisionKey="",""Guid"":""66b0eec3-7646-4d71-a3da-2baad20e42ae"",""CreatedDate"":""2017-04-15T20:18:06.8181908Z"",""Version"":0,""PersistenceKey"":""00000000-0000-0000-0000-000000000000""},""Video"":{""Id"":5,""CreatedByUserId"":null,""ClientId"":3,""ClonedFromId"":null,""ClientGuid"":""0902543c-39b5-40e8-84ef-8ebc0c5ce8ef"",""Title"":""Riverside General Hazard Video 1.mp4"",""Description"":null,""Duration"":0.0,""ResultsCount"":671,""CandidateResultsCount"":61,""CandidatesCount"":67,""ExamCount"":17,""HazardCount"":11,""RevisionKey"":""636271018048836334"",""ScreenshotUrl"":""http://127.0.0.1:10000/devstoreaccount1/0902543c-39b5-40e8-84ef-8ebc0c5ce8ef/b8a8e29d-e485-4ce6-b22d-2e1d990f6b84-screenshot?sv=2017-04-17&sr=b&si=ReadOnly&sig=fxLwYlaSwEYfqWbt84Tn5I4U5LRQlLGvaCydJvrqzzQ%3D&revisionKey=636271018048836334"",""ScreenshotMiniUrl"":""http://127.0.0.1:10000/devstoreaccount1/0902543c-39b5-40e8-84ef-8ebc0c5ce8ef/b8a8e29d-e485-4ce6-b22d-2e1d990f6b84-screenshot-mini?sv=2017-04-17&sr=b&si=ReadOnly&sig=08%2Fgx3jC70N48NsW07UIz%2B1SUkYW8jLmxgQ0CdKNfPM%3D&revisionKey=636271018048836334"",""Guid"":""b8a8e29d-e485-4ce6-b22d-2e1d990f6b84"",""CreatedDate"":""2017-04-06T18:56:44.8836334Z"",""Version"":0,""PersistenceKey"":""00000000-0000-0000-0000-000000000000""},""Results"":[{""ExamId"":2,""VideoId"":5,""ClientId"":3,""CandidateId"":""2b2b0e44-4579-4965-8e3a-097e6684b767"",""CandidateResultId"":6,""HazardId"":23,""ExamCandidateId"":2,""CreatedByUserId"":null,""Success"":true,""ClientGuid"":""0902543c-39b5-40e8-84ef-8ebc0c5ce8ef"",""X"":33.5588957134046,""Y"":85.527835497611889,""Time"":23.9148953,""Guid"":""00000000-0000-0000-0000-000000000000"",""Id"":44,""CreatedDate"":""0001-01-01T00:00:00Z"",""Version"":0,""PersistenceKey"":""00000000-0000-0000-0000-000000000000"",""Hazard"":{""Id"":23,""ClonedFromId"":null,""VideoId"":5,""ClientId"":3,""CreatedByUserId"":null,""ClientGuid"":""0902543c-39b5-40e8-84ef-8ebc0c5ce8ef"",""VideoGuid"":""b8a8e29d-e485-4ce6-b22d-2e1d990f6b84"",""Title"":""Access & Egress Hazard"",""Description"":""Poor house keeping results in a slip and trip hazard."",""TimeFrom"":22.177,""Duration"":2.0,""Left"":5.09,""Top"":7.99,""Width"":82.45,""Height"":76.64,""RevisionKey"":""636530746589620603"",""ImageUrl"":""http://127.0.0.1:10000/devstoreaccount1/b8a8e29d-e485-4ce6-b22d-2e1d990f6b84/1722266f-4174-4621-be90-219b74386226-screenshot?sv=2017-04-17&sr=b&si=ReadOnly&sig=3secFepmqzQ6c0kxmJDV8%2FpNowJKACBqIz8xPpCTnXY%3D&revisionKey=636530746589620603"",""Guid"":""1722266f-4174-4621-be90-219b74386226"",""CreatedDate"":""0001-01-01T00:00:00Z"",""Version"":0,""PersistenceKey"":""00000000-0000-0000-0000-000000000000""}},{""ExamId"":2,""VideoId"":5,""ClientId"":3,""CandidateId"":""2b2b0e44-4579-4965-8e3a-097e6684b767"",""CandidateResultId"":6,""HazardId"":24,""ExamCandidateId"":2,""CreatedByUserId"":null,""Success"":true,""ClientGuid"":""0902543c-39b5-40e8-84ef-8ebc0c5ce8ef"",""X"":33.5588957134046,""Y"":85.527835497611889,""Time"":23.9148953,""Guid"":""00000000-0000-0000-0000-000000000000"",""Id"":45,""CreatedDate"":""0001-01-01T00:00:00Z"",""Version"":0,""PersistenceKey"":""00000000-0000-0000-0000-000000000000"",""Hazard"":{""Id"":24,""ClonedFromId"":null,""VideoId"":5,""ClientId"":3,""CreatedByUserId"":null,""ClientGuid"":""0902543c-39b5-40e8-84ef-8ebc0c5ce8ef"",""VideoGuid"":""b8a8e29d-e485-4ce6-b22d-2e1d990f6b84"",""Title"":""Poor Housekeeping Hazard"",""Description"":""Poor house keeping under scaffold resulting in potential sip trip and fall hazards.  In 2015 poor housekeeping resulted in 20% of all Mount Anvil accidents."",""TimeFrom"":22.568,""Duration"":2.362,""Left"":19.06,""Top"":4.01,""Width"":72.91,""Height"":82.89,""RevisionKey"":""636530746590663675"",""ImageUrl"":""http://127.0.0.1:10000/devstoreaccount1/b8a8e29d-e485-4ce6-b22d-2e1d990f6b84/c342d8f5-49ab-48a4-b17e-47233d1fe6ea-screenshot?sv=2017-04-17&sr=b&si=ReadOnly&sig=rlrbf3Jek4EX4mUZ8aaFOBxTrToYyKh6QZTeDNFAAfM%3D&revisionKey=636530746590663675"",""Guid"":""c342d8f5-49ab-48a4-b17e-47233d1fe6ea"",""CreatedDate"":""0001-01-01T00:00:00Z"",""Version"":0,""PersistenceKey"":""00000000-0000-0000-0000-000000000000""}},{""ExamId"":2,""VideoId"":5,""ClientId"":3,""CandidateId"":""2b2b0e44-4579-4965-8e3a-097e6684b767"",""CandidateResultId"":6,""HazardId"":14,""ExamCandidateId"":2,""CreatedByUserId"":null,""Success"":false,""ClientGuid"":""0902543c-39b5-40e8-84ef-8ebc0c5ce8ef"",""X"":0.0,""Y"":0.0,""Time"":0.0,""Guid"":""00000000-0000-0000-0000-000000000000"",""Id"":46,""CreatedDate"":""0001-01-01T00:00:00Z"",""Version"":0,""PersistenceKey"":""00000000-0000-0000-0000-000000000000"",""Hazard"":{""Id"":14,""ClonedFromId"":null,""VideoId"":5,""ClientId"":3,""CreatedByUserId"":null,""ClientGuid"":""0902543c-39b5-40e8-84ef-8ebc0c5ce8ef"",""VideoGuid"":""b8a8e29d-e485-4ce6-b22d-2e1d990f6b84"",""Title"":""Manual Handling Hazard"",""Description"":""Poor manual handling techniques.  Two man lift required"",""TimeFrom"":4.44,""Duration"":2.0,""Left"":33.58,""Top"":3.69,""Width"":34.91,""Height"":57.72,""RevisionKey"":""636530746578723292"",""ImageUrl"":""http://127.0.0.1:10000/devstoreaccount1/b8a8e29d-e485-4ce6-b22d-2e1d990f6b84/e969b4b0-1403-49e3-8eb6-b033d52ea7e6-screenshot?sv=2017-04-17&sr=b&si=ReadOnly&sig=IbCLNa0N23rT67qitRdBAJW770qAZgjk3lMApHcsVlo%3D&revisionKey=636530746578723292"",""Guid"":""e969b4b0-1403-49e3-8eb6-b033d52ea7e6"",""CreatedDate"":""0001-01-01T00:00:00Z"",""Version"":0,""PersistenceKey"":""00000000-0000-0000-0000-000000000000""}},{""ExamId"":2,""VideoId"":5,""ClientId"":3,""CandidateId"":""2b2b0e44-4579-4965-8e3a-097e6684b767"",""CandidateResultId"":6,""HazardId"":15,""ExamCandidateId"":2,""CreatedByUserId"":null,""Success"":false,""ClientGuid"":""0902543c-39b5-40e8-84ef-8ebc0c5ce8ef"",""X"":0.0,""Y"":0.0,""Time"":0.0,""Guid"":""00000000-0000-0000-0000-000000000000"",""Id"":47,""CreatedDate"":""0001-01-01T00:00:00Z"",""Version"":0,""PersistenceKey"":""00000000-0000-0000-0000-000000000000"",""Hazard"":{""Id"":15,""ClonedFromId"":null,""VideoId"":5,""ClientId"":3,""CreatedByUserId"":null,""ClientGuid"":""0902543c-39b5-40e8-84ef-8ebc0c5ce8ef"",""VideoGuid"":""b8a8e29d-e485-4ce6-b22d-2e1d990f6b84"",""Title"":""Access and Egress Hazard"",""Description"":""Access route is blocked resulting in a slip and trip hazard.  Rebar Caps not in place."",""TimeFrom"":5.032,""Duration"":3.812,""Left"":40.19,""Top"":34.56,""Width"":31.13,""Height"":50.67,""RevisionKey"":""636530746582583406"",""ImageUrl"":""http://127.0.0.1:10000/devstoreaccount1/b8a8e29d-e485-4ce6-b22d-2e1d990f6b84/56f462ea-cd18-4807-9bbd-49e7a06acb7a-screenshot?sv=2017-04-17&sr=b&si=ReadOnly&sig=xjqw4unDhacJnAk%2FInl0Pb24ZjQrL9sZs4k7C150GaU%3D&revisionKey=636530746582583406"",""Guid"":""56f462ea-cd18-4807-9bbd-49e7a06acb7a"",""CreatedDate"":""0001-01-01T00:00:00Z"",""Version"":0,""PersistenceKey"":""00000000-0000-0000-0000-000000000000""}},{""ExamId"":2,""VideoId"":5,""ClientId"":3,""CandidateId"":""2b2b0e44-4579-4965-8e3a-097e6684b767"",""CandidateResultId"":6,""HazardId"":20,""ExamCandidateId"":2,""CreatedByUserId"":null,""Success"":false,""ClientGuid"":""0902543c-39b5-40e8-84ef-8ebc0c5ce8ef"",""X"":0.0,""Y"":0.0,""Time"":0.0,""Guid"":""00000000-0000-0000-0000-000000000000"",""Id"":48,""CreatedDate"":""0001-01-01T00:00:00Z"",""Version"":0,""PersistenceKey"":""00000000-0000-0000-0000-000000000000"",""Hazard"":{""Id"":20,""ClonedFromId"":null,""VideoId"":5,""ClientId"":3,""CreatedByUserId"":null,""ClientGuid"":""0902543c-39b5-40e8-84ef-8ebc0c5ce8ef"",""VideoGuid"":""b8a8e29d-e485-4ce6-b22d-2e1d990f6b84"",""Title"":""Access & Egress Hazard"",""Description"":""Banding left in walkway resulting in a slip and trip hazard"",""TimeFrom"":10.199,""Duration"":2.0,""Left"":45.66,""Top"":11.68,""Width"":54.15,""Height"":77.18,""RevisionKey"":""636530746586470897"",""ImageUrl"":""http://127.0.0.1:10000/devstoreaccount1/b8a8e29d-e485-4ce6-b22d-2e1d990f6b84/8f3a259f-a2ad-4151-800a-64f4918b9ac9-screenshot?sv=2017-04-17&sr=b&si=ReadOnly&sig=M64u0XLS2v9HxN3Ux31yqBcP0%2FEbM9wEGL3UZaOMll8%3D&revisionKey=636530746586470897"",""Guid"":""8f3a259f-a2ad-4151-800a-64f4918b9ac9"",""CreatedDate"":""0001-01-01T00:00:00Z"",""Version"":0,""PersistenceKey"":""00000000-0000-0000-0000-000000000000""}},{""ExamId"":2,""VideoId"":5,""ClientId"":3,""CandidateId"":""2b2b0e44-4579-4965-8e3a-097e6684b767"",""CandidateResultId"":6,""HazardId"":21,""ExamCandidateId"":2,""CreatedByUserId"":null,""Success"":false,""ClientGuid"":""0902543c-39b5-40e8-84ef-8ebc0c5ce8ef"",""X"":0.0,""Y"":0.0,""Time"":0.0,""Guid"":""00000000-0000-0000-0000-000000000000"",""Id"":49,""CreatedDate"":""0001-01-01T00:00:00Z"",""Version"":0,""PersistenceKey"":""00000000-0000-0000-0000-000000000000"",""Hazard"":{""Id"":21,""ClonedFromId"":null,""VideoId"":5,""ClientId"":3,""CreatedByUserId"":null,""ClientGuid"":""0902543c-39b5-40e8-84ef-8ebc0c5ce8ef"",""VideoGuid"":""b8a8e29d-e485-4ce6-b22d-2e1d990f6b84"",""Title"":""Housekeeping Hazard"",""Description"":""Materials stored poorly, resulting in a fire load hazard and unsafe access when unloading"",""TimeFrom"":15.894,""Duration"":2.0,""Left"":56.42,""Top"":0.0,""Width"":43.21,""Height"":78.86,""RevisionKey"":""636530746587679274"",""ImageUrl"":""http://127.0.0.1:10000/devstoreaccount1/b8a8e29d-e485-4ce6-b22d-2e1d990f6b84/c244653f-b0c6-4610-a4da-159dc664181c-screenshot?sv=2017-04-17&sr=b&si=ReadOnly&sig=BdI8JQGlmrpgkjjfk3r5NC7Tfrs04kKfZzQsj0ZqCPI%3D&revisionKey=636530746587679274"",""Guid"":""c244653f-b0c6-4610-a4da-159dc664181c"",""CreatedDate"":""0001-01-01T00:00:00Z"",""Version"":0,""PersistenceKey"":""00000000-0000-0000-0000-000000000000""}},{""ExamId"":2,""VideoId"":5,""ClientId"":3,""CandidateId"":""2b2b0e44-4579-4965-8e3a-097e6684b767"",""CandidateResultId"":6,""HazardId"":22,""ExamCandidateId"":2,""CreatedByUserId"":null,""Success"":false,""ClientGuid"":""0902543c-39b5-40e8-84ef-8ebc0c5ce8ef"",""X"":0.0,""Y"":0.0,""Time"":0.0,""Guid"":""00000000-0000-0000-0000-000000000000"",""Id"":50,""CreatedDate"":""0001-01-01T00:00:00Z"",""Version"":0,""PersistenceKey"":""00000000-0000-0000-0000-000000000000"",""Hazard"":{""Id"":22,""ClonedFromId"":null,""VideoId"":5,""ClientId"":3,""CreatedByUserId"":null,""ClientGuid"":""0902543c-39b5-40e8-84ef-8ebc0c5ce8ef"",""VideoGuid"":""b8a8e29d-e485-4ce6-b22d-2e1d990f6b84"",""Title"":""First Aid Hazard"",""Description"":""Eye wash station missing eye wash bottles, resulting in a first aid hazard."",""TimeFrom"":19.208,""Duration"":2.0,""Left"":41.13,""Top"":8.99,""Width"":45.66,""Height"":60.74,""RevisionKey"":""636530746588667798"",""ImageUrl"":""http://127.0.0.1:10000/devstoreaccount1/b8a8e29d-e485-4ce6-b22d-2e1d990f6b84/3b3f8a97-eeea-410c-88be-f207dd8b0d6b-screenshot?sv=2017-04-17&sr=b&si=ReadOnly&sig=NujaVEhwJ84XL2bKiRqICzvRMl4lcyl08tpYTuB2Z20%3D&revisionKey=636530746588667798"",""Guid"":""3b3f8a97-eeea-410c-88be-f207dd8b0d6b"",""CreatedDate"":""0001-01-01T00:00:00Z"",""Version"":0,""PersistenceKey"":""00000000-0000-0000-0000-000000000000""}},{""ExamId"":2,""VideoId"":5,""ClientId"":3,""CandidateId"":""2b2b0e44-4579-4965-8e3a-097e6684b767"",""CandidateResultId"":6,""HazardId"":25,""ExamCandidateId"":2,""CreatedByUserId"":null,""Success"":false,""ClientGuid"":""0902543c-39b5-40e8-84ef-8ebc0c5ce8ef"",""X"":0.0,""Y"":0.0,""Time"":0.0,""Guid"":""00000000-0000-0000-0000-000000000000"",""Id"":51,""CreatedDate"":""0001-01-01T00:00:00Z"",""Version"":0,""PersistenceKey"":""00000000-0000-0000-0000-000000000000"",""Hazard"":{""Id"":25,""ClonedFromId"":null,""VideoId"":5,""ClientId"":3,""CreatedByUserId"":null,""ClientGuid"":""0902543c-39b5-40e8-84ef-8ebc0c5ce8ef"",""VideoGuid"":""b8a8e29d-e485-4ce6-b22d-2e1d990f6b84"",""Title"":""Access & Egress Hazard"",""Description"":""Blocked Access & egress Hazard resulting in a snag hazard and restricted fire escape."",""TimeFrom"":26.973,""Duration"":2.0,""Left"":25.28,""Top"":0.0,""Width"":37.92,""Height"":72.82,""RevisionKey"":""636530746591535528"",""ImageUrl"":""http://127.0.0.1:10000/devstoreaccount1/b8a8e29d-e485-4ce6-b22d-2e1d990f6b84/2a843cb6-cbaa-4a13-88bd-32a6e068afc1-screenshot?sv=2017-04-17&sr=b&si=ReadOnly&sig=HluAh2%2BcoypNpChVdHt9UcC8bFBfEdM596O0FYOI3kM%3D&revisionKey=636530746591535528"",""Guid"":""2a843cb6-cbaa-4a13-88bd-32a6e068afc1"",""CreatedDate"":""0001-01-01T00:00:00Z"",""Version"":0,""PersistenceKey"":""00000000-0000-0000-0000-000000000000""}},{""ExamId"":2,""VideoId"":5,""ClientId"":3,""CandidateId"":""2b2b0e44-4579-4965-8e3a-097e6684b767"",""CandidateResultId"":6,""HazardId"":26,""ExamCandidateId"":2,""CreatedByUserId"":null,""Success"":false,""ClientGuid"":""0902543c-39b5-40e8-84ef-8ebc0c5ce8ef"",""X"":0.0,""Y"":0.0,""Time"":0.0,""Guid"":""00000000-0000-0000-0000-000000000000"",""Id"":52,""CreatedDate"":""0001-01-01T00:00:00Z"",""Version"":0,""PersistenceKey"":""00000000-0000-0000-0000-000000000000"",""Hazard"":{""Id"":26,""ClonedFromId"":null,""VideoId"":5,""ClientId"":3,""CreatedByUserId"":null,""ClientGuid"":""0902543c-39b5-40e8-84ef-8ebc0c5ce8ef"",""VideoGuid"":""b8a8e29d-e485-4ce6-b22d-2e1d990f6b84"",""Title"":""Falling Object Hazard"",""Description"":""Excessive gap in scaffold board results in a falling object hazard."",""TimeFrom"":29.791,""Duration"":2.0,""Left"":3.21,""Top"":44.56,""Width"":40.75,""Height"":44.63,""RevisionKey"":""636530746592293584"",""ImageUrl"":""http://127.0.0.1:10000/devstoreaccount1/b8a8e29d-e485-4ce6-b22d-2e1d990f6b84/6c1563a6-4621-4569-9644-1267240f79ed-screenshot?sv=2017-04-17&sr=b&si=ReadOnly&sig=s%2FPYEX86%2FfpQhC4cuAsOzX4RVk41osDI5mP%2FpguVo7w%3D&revisionKey=636530746592293584"",""Guid"":""6c1563a6-4621-4569-9644-1267240f79ed"",""CreatedDate"":""0001-01-01T00:00:00Z"",""Version"":0,""PersistenceKey"":""00000000-0000-0000-0000-000000000000""}},{""ExamId"":2,""VideoId"":5,""ClientId"":3,""CandidateId"":""2b2b0e44-4579-4965-8e3a-097e6684b767"",""CandidateResultId"":6,""HazardId"":27,""ExamCandidateId"":2,""CreatedByUserId"":null,""Success"":false,""ClientGuid"":""0902543c-39b5-40e8-84ef-8ebc0c5ce8ef"",""X"":0.0,""Y"":0.0,""Time"":0.0,""Guid"":""00000000-0000-0000-0000-000000000000"",""Id"":53,""CreatedDate"":""0001-01-01T00:00:00Z"",""Version"":0,""PersistenceKey"":""00000000-0000-0000-0000-000000000000"",""Hazard"":{""Id"":27,""ClonedFromId"":null,""VideoId"":5,""ClientId"":3,""CreatedByUserId"":null,""ClientGuid"":""0902543c-39b5-40e8-84ef-8ebc0c5ce8ef"",""VideoGuid"":""b8a8e29d-e485-4ce6-b22d-2e1d990f6b84"",""Title"":""Fall From Height Hazard"",""Description"":""No protection to ladder access resulting in falling object and material hazards."",""TimeFrom"":29.873,""Duration"":3.269,""Left"":6.22,""Top"":21.74,""Width"":48.11,""Height"":56.71,""RevisionKey"":""636530746593336270"",""ImageUrl"":""http://127.0.0.1:10000/devstoreaccount1/b8a8e29d-e485-4ce6-b22d-2e1d990f6b84/d089d1c3-4af5-4f61-b2bc-c8a1419a0903-screenshot?sv=2017-04-17&sr=b&si=ReadOnly&sig=bc3MjqqwdrE5jcqo9ZgSSwMBkfyqOcIFoARKBAzPYrs%3D&revisionKey=636530746593336270"",""Guid"":""d089d1c3-4af5-4f61-b2bc-c8a1419a0903"",""CreatedDate"":""0001-01-01T00:00:00Z"",""Version"":0,""PersistenceKey"":""00000000-0000-0000-0000-000000000000""}},{""ExamId"":2,""VideoId"":5,""ClientId"":3,""CandidateId"":""2b2b0e44-4579-4965-8e3a-097e6684b767"",""CandidateResultId"":6,""HazardId"":28,""ExamCandidateId"":2,""CreatedByUserId"":null,""Success"":false,""ClientGuid"":""0902543c-39b5-40e8-84ef-8ebc0c5ce8ef"",""X"":0.0,""Y"":0.0,""Time"":0.0,""Guid"":""00000000-0000-0000-0000-000000000000"",""Id"":54,""CreatedDate"":""0001-01-01T00:00:00Z"",""Version"":0,""PersistenceKey"":""00000000-0000-0000-0000-000000000000"",""Hazard"":{""Id"":28,""ClonedFromId"":null,""VideoId"":5,""ClientId"":3,""CreatedByUserId"":null,""ClientGuid"":""0902543c-39b5-40e8-84ef-8ebc0c5ce8ef"",""VideoGuid"":""b8a8e29d-e485-4ce6-b22d-2e1d990f6b84"",""Title"":""Falling Object Hazard"",""Description"":""Materials stacked incorrectly against scaffold resulting in a falling object hazard to areas below."",""TimeFrom"":30.988,""Duration"":2.0,""Left"":47.36,""Top"":0.0,""Width"":29.81,""Height"":73.49,""RevisionKey"":""636530746594166859"",""ImageUrl"":""http://127.0.0.1:10000/devstoreaccount1/b8a8e29d-e485-4ce6-b22d-2e1d990f6b84/eae11d57-d52f-44e3-aa4b-99235725f2b2-screenshot?sv=2017-04-17&sr=b&si=ReadOnly&sig=Ls%2FlktPzNodu033WKJ%2FmuE%2BpMnCmrmipHC16ejOBjKE%3D&revisionKey=636530746594166859"",""Guid"":""eae11d57-d52f-44e3-aa4b-99235725f2b2"",""CreatedDate"":""0001-01-01T00:00:00Z"",""Version"":0,""PersistenceKey"":""00000000-0000-0000-0000-000000000000""}}]}]}");
            requestResults.Add(@"http://localhost:28000/odata/MyCustomReports(571202dc-057f-49b8-8681-8450695fc079)",
                @"{""@odata.context"":""anycontext"",""MyId"":""571202dc-057f-49b8-8681-8450695fc079"",""MyUserId"":""5a2642c8-ade3-4162-9f96-d11bae48d02b"",""MyCreatedByUserId"":""5a2642c8-ade3-4162-9f96-d11bae48d02b"",""MyName"":""Backburner2"",""MyEntityType"":""Todo"",""MyIql"":"""",""MySort"":""MyCreatedDate"",""MySortDescending"":true,""MySearch"":false,""MyFields"":null}");
            //requestResults.Add(@"http://localhost:58000/odata/Clients", @"{""@odata.context"":""http://localhost:28000/odata/$metadata#Clients"",""value"":[{""TypeId"":1,""Id"":1,""CreatedByUserId"":""11a51fbc-1894-4b2d-9dd0-598f74da91b5"",""Name"":""Test 1"",""Description"":""Hey"",""Guid"":""2ac6bf93-1c6d-46dd-bd37-5aa9baa6be86"",""CreatedDate"":""2017-11-12T00:00:00Z"",""Version"":1,""PersistenceKey"":""bfe5cbda-b784-44af-a963-f58a684f6a13""}]}");
            Lookup = requestResults;
        }

        private static Dictionary<string, string> Lookup { get; }

        public static string HttpGetResponse(string uri)
        {
            return Lookup.ContainsKey(uri)
                ? Lookup[uri]
                : null;
        }
    }
}