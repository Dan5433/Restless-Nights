public class TasksManager : DifficultySingleton<TasksManager>
{
    public float DifficultyFraction => (float)difficulty / MAX_DIFFICULTY;
}
