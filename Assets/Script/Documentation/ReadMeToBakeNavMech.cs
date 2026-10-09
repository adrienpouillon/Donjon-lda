using UnityEngine;

public class ReadMeToBakeNavMech : ReadMeComponent
{
    private void Reset()
    {
        _DocString = "Pour bake le NavMech rendez-vous dans l'objet NavMesh en question " +
            "\n" +
            "\n" + "Mais avant de cliquer sur \"Bake\" faites les etapes suivantes :" +
            "\n" +
            "\n" + "Allez sur \"Personnage\" et désactivé le " +
            "\n" + "puis aussi \"Objet\" " +
            "\n" + "puis \"Trap\" (si vous souhaitez que l'IA esquive les pièges) " +
            "\n" +
            "\n" + "Puis activé \"InvisibleWallNavMech\"" +
            "\n" +
            "\n" + "Puis \"Bake\" le NavMesh" +
            "\n" +
            "\n" + "Puis réactiver ou redesactiver tout les objets modifié" +
            "\n" + "(Personnage, Objet, Trap, InvisibleWallNavMech,...)";
    }
}
