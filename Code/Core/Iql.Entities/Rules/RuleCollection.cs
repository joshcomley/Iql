using System.Collections.Generic;

namespace Iql.Entities.Rules
{
    //public abstract class BinaryRuleCollection
    public abstract class RuleCollection<TRule> : IRuleCollection<TRule>
    where TRule : IRule
    {
        private bool _rulesDictionaryDelayedInitialized;
        private Dictionary<string, TRule> _rulesDictionaryDelayed;
        private Dictionary<string, TRule> _rulesDictionary { get { if(!_rulesDictionaryDelayedInitialized) { _rulesDictionaryDelayedInitialized = true; _rulesDictionaryDelayed = new Dictionary<string, TRule>(); } return _rulesDictionaryDelayed; } set { _rulesDictionaryDelayedInitialized = true; _rulesDictionaryDelayed = value; } }

        public TRule Get(string key)
        {
            if (_rulesDictionary.ContainsKey(key))
            {
                return _rulesDictionary[key];
            }

            return default(TRule);
        }
        private List<TRule> _all;

        public List<TRule> All => _all = _all ?? new List<TRule>();

        IEnumerable<TRule> IRuleCollection<TRule>.All => All;

        public TRule Add(TRule rule)
        {
            if (_rulesDictionary.ContainsKey(rule.Key))
            {
                var old = _rulesDictionary[rule.Key];
                _rulesDictionary[rule.Key] = rule;
                All.Remove(old);
            }
            else
            {
                _rulesDictionary.Add(rule.Key, rule);
            }

            All.Add(rule);
            return rule;
        }

        public void Remove(string key)
        {
            if (_rulesDictionary.ContainsKey(key))
            {
                var old = _rulesDictionary[key];
                _rulesDictionary.Remove(key);
                All.Remove(old);
            }
        }
        TRule IRuleCollection<TRule>.Get(string key) => Get(key);

        TRule IRuleCollection<TRule>.Add(object rule)
        {
            return Add((TRule) rule);
            //var compiled = (Func<object, bool> )expression.Compile();
            //return Add(e => compiled(e), key, message);
        }
    }
}