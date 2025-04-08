using UnityEditor.UIElements;
using UnityEngine;

public static class Global
{
    // This class hold universal data type that are frequently use or compare to. Prefer common use by ANY type of object/script

    /* Layer & LayerMask name */
    private static string playerLayerName = "Player";
    private static string groundLayerName = "Ground";
    private static string ghostLayerName = "Ghost";
    private static string projectileLayerName = "ProjectileLayer";

    /* LayerMasks */
    public static readonly LayerMask playerLayer = LayerMask.GetMask(playerLayerName);
    public static readonly LayerMask groundLayer = LayerMask.GetMask(groundLayerName);
    public static readonly LayerMask ghostLayer = LayerMask.GetMask(ghostLayerName);
    public static readonly LayerMask projectileLayer = LayerMask.GetMask(projectileLayerName);

    /* Layers */
    public static readonly int playerLayerIndex = LayerMask.NameToLayer(playerLayerName); 
    public static readonly int groundLayerIndex = LayerMask.NameToLayer(groundLayerName); 
    public static readonly int ghostLayerIndex = LayerMask.NameToLayer(ghostLayerName); 
    public static readonly int projectileLayerIndex = LayerMask.NameToLayer(projectileLayerName);

    /* Scene */
    public static readonly string mainMenuScene = "Main-MainMenu-Scene";
    public static readonly string skillSelectionScene = "Main-GamePlay-Scene"; //"Main-SkillSelection-Scene";
    public static readonly string gamePlayScene = "Main-GamePlay-Scene";
    public static readonly string playerOneWinScene;
    public static readonly string playerTwoWinScene;

    /* Tags */
    public static readonly string playerOneTag = "Player1";
    public static readonly string playerTwoTag = "Player2";

    /* Input Action Maps */
    public static readonly string playerOneInputMap = "Player1";
    public static readonly string playerTwoInputMap = "Player2";

}
