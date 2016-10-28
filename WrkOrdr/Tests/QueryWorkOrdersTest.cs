﻿using System;
using System.Collections.Generic;
using System.Linq;
using Ploeh.AutoFixture;
using Should;
using WrkOrdr.Configuration;
using WrkOrdr.TestObjects;
using WrkOrdr.TestObjects.Handlers;
using WrkOrdr.TestObjects.Messages;

namespace WrkOrdr.Tests
{
    public class QueryWorkOrdersTest:BaseTesting<WorkOrderQueryHandler>
    {
        public override void FixtureSetup(IFixture fixture)
        {
            base.FixtureSetup(fixture);
            RegisterDatabase();
        }

        public void can_get_all_work_orders()
        {

            var handler = _fixture.Create<WorkOrderHandler>();

            var createMessage = _fixture.Build<CreateWorkOrderMessage>()
                .With(x => x.Status, WorkOrderStatus.NotStarted)
                .Create();

            handler.Handle(createMessage);

            var itemMessage = _fixture
                .Build<CreateWorkOrderItemMessage>()
                .With(x => x.Id, createMessage.Id)
                .With(x => x.Status, WorkItemStatus.New)
                .With(x => x.Details, _fixture.CreateMany<KeyValuePair<string, object>>(10).ToDictionary(k => k.Key, v => v.Value))
                .Create();

            handler.Handle(itemMessage);


            var workOrders = Sut.Get();
            workOrders.Count.ShouldBeGreaterThan(1);
        }
    }
}