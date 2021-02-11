import 'package:flutter/material.dart';
// import 'dart:mirrors';

void main() {
  runApp(MyApp());
}

class MyApp extends StatelessWidget {
  // This widget is the root of your application.
  @override
  Widget build(BuildContext context) {
    return MaterialApp(
      title: 'Flutter Demo',
      theme: ThemeData(
        // This is the theme of your application.
        //
        // Try running your application with "flutter run". You'll see the
        // application has a blue toolbar. Then, without quitting the app, try
        // changing the primarySwatch below to Colors.green and then invoke
        // "hot reload" (press "r" in the console where you ran "flutter run",
        // or simply save your changes to "hot reload" in a Flutter IDE).
        // Notice that the counter didn't reset back to zero; the application
        // is not restarted.
        primarySwatch: Colors.blue,
        // This makes the visual density adapt to the platform that you run
        // the app on. For desktop platforms, the controls will be smaller and
        // closer together (more dense) than on mobile platforms.
        visualDensity: VisualDensity.adaptivePlatformDensity,
      ),
      home: MyHomePage(title: 'Flutter Demo Home Page'),
    );
  }
}

class MyHomePage extends StatefulWidget {
  MyHomePage({Key key, this.title}) : super(key: key);

  // This widget is the home page of your application. It is stateful, meaning
  // that it has a State object (defined below) that contains fields that affect
  // how it looks.

  // This class is the configuration for the state. It holds the values (in this
  // case the title) provided by the parent (in this case the App widget) and
  // used by the build method of the State. Fields in a Widget subclass are
  // always marked "final".

  final String title;

  @override
  _MyHomePageState createState() => _MyHomePageState();
}

class PropertyPathMapper {
  String path;

  void add(String part) {
    if (path == null) {
      path = part;
    } else {
      path = "$path.$part";
    }
  }
}

class EntityBase<T extends EntityBase<T>> {
  Map<String, Object> _valueMap = new Map<String, Object>();
  PropertyPathMapper _mapper;
  T withMapper(PropertyPathMapper mapper) {
    _mapper = mapper;
    return this;
  }

  T getValue<T>(String name) {
    _mapper?.add(name);
    if (!_valueMap.containsKey(name)) {
      setValue(name, null);
    }
    var isValueType = T == String || T == int || T == bool || T == double;
    if (_mapper != null && !isValueType) {
      setValue(
          name, _valueMap[name] ?? new SomeOtherThing().withMapper(_mapper));
    }
    return _valueMap[name];
  }

  setValue<T>(String name, T value) {
    _valueMap[name] = value;
  }
}

class SomeThing extends EntityBase<SomeThing> {
  SomeOtherThing get myThing => getValue("myThing");
  set myThing(SomeOtherThing value) => setValue("myThing", value);
}

class SomeOtherThing extends EntityBase<SomeOtherThing> {
  int get count => getValue("count");
  set count(int value) => setValue("count", value);

  String get name => getValue("name");
  set name(String value) => setValue("name", value);
}

enum Operand {
  IsEqualTo,
  IsNotEqualTo,
  IsLessThan,
  IsLessThanOrEqualTo,
  IsGreaterThan,
  IsGreaterThanOrEqualTo,
}

class IqlTypeUtility {
  // (<T>() => T)<T>()
  static Type typeOf<T>() => T;
}

class FactoryHelper {
  static Map<Type, Function(PropertyPathMapper)> _factories;

  static Map<Type, Function(PropertyPathMapper)> get factories =>
      _factories = _factories ?? _buildFactories();

  static set factories(Map<Type, Function(PropertyPathMapper)> factories) {
    _factories = factories;
  }

  static T newInstance<T>(PropertyPathMapper mapper) {
    return factories[IqlTypeUtility.typeOf<T>()](mapper);
  }

  static Map<Type, Function(PropertyPathMapper)> _buildFactories() {
    var factories = new Map<Type, Function(PropertyPathMapper)>();
    factories[IqlTypeUtility.typeOf<SomeThing>()] =
        (PropertyPathMapper m) => new SomeThing().withMapper(m);
    factories[IqlTypeUtility.typeOf<SomeOtherThing>()] =
        (PropertyPathMapper m) => new SomeOtherThing().withMapper(m);
    return factories;
  }
}

class DataSet<T> {
  String whereValue<TValue>(
      TValue Function(T) left, Operand operand, TValue right) {
    var mapper = new PropertyPathMapper();
    left(FactoryHelper.factories[IqlTypeUtility.typeOf<T>()](mapper) as T);
    var valueString = right == null ? "null" : right.toString();
    if (IqlTypeUtility.typeOf<TValue>() == String) {
      valueString = "\"$valueString\"";
    }
    return "${mapper.path} ${operand.toJs()} $valueString";
  }

  String pathOf(dynamic Function(T) fn) {
    var mapper = new PropertyPathMapper();
    fn(FactoryHelper.newInstance(mapper));
    return mapper.path;
  }

  String whereQuery<TValue>(
      TValue Function(T) left, Operand operand, TValue Function(T) right) {
    return "${pathOf(left)} ${operand.toJs()} ${pathOf(right)}";
  }
}

extension OperandName on Operand {
  String toJs() {
    switch (this) {
      case Operand.IsEqualTo:
        return "==";
      case Operand.IsNotEqualTo:
        return "!=";
      case Operand.IsGreaterThan:
        return ">";
      case Operand.IsGreaterThanOrEqualTo:
        return ">=";
      case Operand.IsLessThan:
        return "<";
      case Operand.IsLessThanOrEqualTo:
        return "<=";
    }
    return "";
  }
}

class _MyHomePageState extends State<MyHomePage> {
  String _name = "abc";

  void _incrementCounter() {
    setState(() {
      var dataSet = new DataSet<SomeThing>();
      var x = new SomeThing();
      x.myThing = new SomeOtherThing();
      x.myThing.count = 88;
      var y = x.myThing.count;
      _name = y.toString();
      _name = dataSet.whereQuery<String>((_) => _.myThing.name,
          Operand.IsGreaterThanOrEqualTo, (_) => _.myThing.name);
    });
  }

  @override
  Widget build(BuildContext context) {
    // This method is rerun every time setState is called, for instance as done
    // by the _incrementCounter method above.
    //
    // The Flutter framework has been optimized to make rerunning build methods
    // fast, so that you can just rebuild anything that needs updating rather
    // than having to individually change instances of widgets.
    return Scaffold(
      appBar: AppBar(
        // Here we take the value from the MyHomePage object that was created by
        // the App.build method, and use it to set our appbar title.
        title: Text(widget.title),
      ),
      body: Center(
        // Center is a layout widget. It takes a single child and positions it
        // in the middle of the parent.
        child: Column(
          // Column is also a layout widget. It takes a list of children and
          // arranges them vertically. By default, it sizes itself to fit its
          // children horizontally, and tries to be as tall as its parent.
          //
          // Invoke "debug painting" (press "p" in the console, choose the
          // "Toggle Debug Paint" action from the Flutter Inspector in Android
          // Studio, or the "Toggle Debug Paint" command in Visual Studio Code)
          // to see the wireframe for each widget.
          //
          // Column has various properties to control how it sizes itself and
          // how it positions its children. Here we use mainAxisAlignment to
          // center the children vertically; the main axis here is the vertical
          // axis because Columns are vertical (the cross axis would be
          // horizontal).
          mainAxisAlignment: MainAxisAlignment.center,
          children: <Widget>[
            Text(
              'Your IQL query: $_name',
            ),
          ],
        ),
      ),
      floatingActionButton: FloatingActionButton(
        onPressed: _incrementCounter,
        tooltip: 'Increment',
        child: Icon(Icons.add),
      ), // This trailing comma makes auto-formatting nicer for build methods.
    );
  }
}
