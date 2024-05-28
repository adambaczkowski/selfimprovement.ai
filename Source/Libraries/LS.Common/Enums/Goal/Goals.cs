public enum Goals
{
    BodyWorkout = 0,
    Programming = 1,
}

public static class GoalsExtensionMethods {
    public static string ToFriendlyString(this Goals e) {
        switch (e) {
            case Goals.BodyWorkout:
                return "body workout";
            case Goals.Programming:
                return "programming";
            default:
                return "nothing";
        }
    }
}