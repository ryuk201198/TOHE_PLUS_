namespace TOHE;

public class StringOptionItem(int id, string name, int defaultValue, TabGroup tab, bool isSingleValue, string[] selections) : OptionItem(id, name, defaultValue, tab, isSingleValue)
{
    // 必須情報
    public IntegerValueRule Rule = (0, selections.Length - 1, 1);
    public string[] Selections = selections;

    public static StringOptionItem Create(
        int id, string name, string[] selections, int defaultIndex, TabGroup tab, bool isSingleValue
    )
    {
        return new StringOptionItem(
            id, name, defaultIndex, tab, isSingleValue, selections
        );
    }

    // Getter
    public override int GetInt() => Rule.GetValueByIndex(CurrentValue);
    public override float GetFloat() => Rule.GetValueByIndex(CurrentValue);
    public override string GetString()
    {
        return Translator.GetString(Selections[Rule.GetValueByIndex(CurrentValue)]);
    }
    public int GetChance()
    {
        //For 0% or 100%
        if (Selections.Length == 2) return CurrentValue * 100;

        //TOHE’s career generation mode
        if (Selections.Length == 3) return CurrentValue;

        //For 0% to 100% or 5% to 100%
        var offset = Options.rates.Length - Selections.Length;
        var index = CurrentValue + offset;
        var rate = index * 5;
        return rate;
    }
    public override int GetValue()
        => Rule.RepeatIndex(base.GetValue());

    // Setter
    public override void SetValue(int value, bool doSync = true)
    {
        base.SetValue(Rule.RepeatIndex(value), doSync);
    }
}