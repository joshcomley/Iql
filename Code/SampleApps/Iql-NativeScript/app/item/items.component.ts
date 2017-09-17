import { Component, OnInit } from "@angular/core";

import { Item } from "./item";
import { ItemService } from "./item.service";
import { IqlQueryableAdapter } from "../Iql.Queryable/IqlQueryableAdapter";
import { JavaScriptExpressionToIqlConverter } from "../Iql.JavaScript/JavaScriptExpressionToIql/Expressions/JavaScript/JavaScriptExpressionToIqlConverter";
import { Person } from "../Code/Person";
import { AppDbContext } from "../Code/AppDbContext";

@Component({
    selector: "ns-items",
    moduleId: module.id,
    templateUrl: "./items.component.html",
})
export class ItemsComponent implements OnInit {
    items: Item[];
    name: string = "Not done..";
    // This pattern makes use of Angular’s dependency injection implementation to inject an instance of the ItemService service into this class. 
    // Angular knows about this service because it is included in your app’s main NgModule, defined in app.module.ts.
    constructor(private itemService: ItemService) { }

    async ngOnInit() {
        this.items = this.itemService.getItems();
        IqlQueryableAdapter.ExpressionConverter = () => new JavaScriptExpressionToIqlConverter();
        let db = new AppDbContext();
        let josh = new Person();
        josh.Name = "Josh";
        josh.Age = 29;
        josh.Id = -3;
        db.People.Add(josh);
        await db.SaveChanges();

        let result = await db.People
            .OrderBy(p => p.Id)
            .Where(p => p.Name == "Josh")
            .ToList();
        this.name = result[0].Name;
    }

    async doSomething(): Promise<string> {
        let p = new Promise<string>(resolve => {
            setTimeout(() => {
                resolve("Done!");
            }, 2000);
        });
        return p;
    }
}