import { ODataQuery } from './Iql/Iql.OData/Queryable/ODataQuery';
import { TypeOf } from './Iql/TsUtility/TypeOf';
import { IODataQuery } from './Iql/Iql.OData/Queryable/IODataQuery';
import { ODataQueryableAdapter } from './Iql/Iql.OData/Queryable/ODataQueryableAdapter';
import { QueryableExtensions } from './Iql/Iql.Queryable/QueryableExtensions';
import { Types } from './Iql/TsUtility/Types';

import { OnInit, Component } from "@angular/core";
import { QueryExpression } from "./Iql/Iql.Queryable/Expressions/QueryExpressions/QueryExpression";
import { IqlQueryableAdapter } from "./Iql/Iql.Queryable/IqlQueryableAdapter";
import { JavaScriptExpressionToIqlConverter } from "./Iql/Iql.JavaScript/JavaScriptExpressionToIql/Expressions/JavaScript/JavaScriptExpressionToIqlConverter";
import { WhereQueryExpression } from "./Iql/Iql.Queryable/Expressions/QueryExpressions/WhereQueryExpression";
import { AndQueryExpression } from "./Iql/Iql.Queryable/Expressions/QueryExpressions/AndQueryExpression";
import { QueryFilterEval } from "./QueryEval";
import { OrQueryExpression } from "./Iql/Iql.Queryable/Expressions/QueryExpressions/OrQueryExpression";
import { JavaScriptQueryableAdapter } from "./Iql/Iql.JavaScript/QueryToJavaScript/JavaScriptQueryableAdapter";
import { JavaScriptQuery } from "./Iql/Iql.JavaScript/QueryToJavaScript/JavaScriptQuery";
import { Person } from "./Code/Person";
import { AppDbContext } from "./Code/AppDbContext";

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
  public individual: Person;
  public people: Array<Person>;
  public odata: string;
  public javascript: string;
  async ngOnInit() {
    this.title = "Yep";
    IqlQueryableAdapter.ExpressionConverter = () => new JavaScriptExpressionToIqlConverter();



    let db = new AppDbContext();
    let josh = new Person();
    josh.Name = "Josh";
    josh.Age = 27;
    db.People.Add(josh);
    let age = { x: 25 };
    this.individual = (await db.People.WithKey(2)).Data;
    await db.SaveChanges();
    // let query = db.People
    //   .OrderBy(p => p.Age);
    // query = query
    //   .Where(p => p.Age > age.x || p.Name.endsWith("osh"), eval(QueryFilterEval));

    let lookup = "h";
    let queryExpression1 = new WhereQueryExpression<Person>(p => p.Age === 21, eval(QueryFilterEval));
    let queryExpression2 = new WhereQueryExpression<Person>(p => p.Name.startsWith(lookup), eval(QueryFilterEval));
    let queryExpression = new AndQueryExpression(eval(QueryFilterEval), queryExpression1, queryExpression2);
    let endsWithVariable = "M";
    let queryExpression3 = new OrQueryExpression(eval(QueryFilterEval),
      queryExpression,
      new AndQueryExpression(eval(QueryFilterEval),
        new WhereQueryExpression<Person>(p => p.Name == "Josh", eval(QueryFilterEval)),
        new WhereQueryExpression<Person>(p => p.Age > 20, eval(QueryFilterEval)))
    );
    let query = db.People.WhereQuery(queryExpression3).OrderBy(p => p.Age);//.WhereQuery(queryExpression3);

    // let orExpression = new OrQueryExpression(eval(QueryFilterEval),
    //   new WhereQueryExpression<Person>(p => p.Name == "Cara", eval(QueryFilterEval)),
    //   new WhereQueryExpression<Person>(p => p.Name.endsWith(lookup), eval(QueryFilterEval)));
    // let query = db.People.WhereQuery(
    //   orExpression
    // );
    let result = await query.ToList();



    this.people = result;
    let q = QueryableExtensions.ToQueryWithAdapter(query, new ODataQueryableAdapter(), db, TypeOf.Interface(`IODataQuery`)) as ODataQuery<Person>;
    this.odata = q.ToODataQuery();
    let jq = QueryableExtensions.ToQueryWithAdapter(query, new JavaScriptQueryableAdapter(), db, TypeOf.Interface(``)) as JavaScriptQuery<Person>;
    this.javascript = jq.ToJavaScriptQuery();
    //await TestDb.Run();
  }

  title = 'app works!';
  constructor() {
    // var fn = (p: Person) => p.Age > 150 && /* Make sure it's me */ p.Name == "Josh" || p.Age == 20;
    // var body = JavaScriptCodeExtractor.ExtractBody(fn.toString());
    // //"p.age > 150 && p.school.name === \"London\" || p.age == 22"
    // let jsp = new JavaScriptExpressionStringToExpressionTreeParser(body.Code);
    // let exp = jsp.parse();
    // let odataParser = new ODataExpressionAdapter();
    //odataParser.Parse()
    //JavaScriptIqlParser.GetJavaScript()
  }
}
