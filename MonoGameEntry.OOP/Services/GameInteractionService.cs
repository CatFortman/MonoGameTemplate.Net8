using MonoGameTemplate.OOP.Entities;

namespace MonoGameTemplate.OOP.Services;

public class GameInteractionService
{
    private readonly AudioService _audio;

    public GameInteractionService(AudioService audio)
    {
        _audio = audio;
    }

    public void HandlePlayerEnemyCollision(Player player, Enemy enemy)
    {
        RespawnEnemy(enemy);
        _audio.PlayCollect();
    }

    public void HandleEnemyWallCollision(Enemy enemy)
    {
        _audio.PlayBounce();
    }

    private void RespawnEnemy(Enemy enemy)
    {
        enemy.Respawn(enemy.Bounds);
    }
}