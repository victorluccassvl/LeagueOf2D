using System;

namespace LeagueOf2D
{
    /*\
     * Static class of debbug information used at every other class
    \*/
    static class Error
    {

        /*\
         * Error Number [01]
         * Info      : Heap overflow
         * Class     : PathHeap
         * Method    : Insert
         * Variables : max_length | length
        \*/
        public static string Message_01 (int max_length, int length)
        {
            string msg = "ERROR [01] - Limite de tamanho do Heap alcançado\n";

            msg += ("             Classe: PathHeap\n");
            msg += ("             Método: Insert\n");
            msg += ("             Variáveis:\n");
            msg += ("                  - max_length = " + max_length + "\n");
            msg += ("                  - lengh      = " + length + "\n");

            return msg;
        }



        /*\
         * Error Number [02]
         * Info      : Heap underflow
         * Class     : PathHeap
         * Method    : Remove
         * Variables : length
        \*/
        public static string Message_02 (int length)
        {
            string msg = "ERROR [02] - Tentativa de remoção no heap vazio\n";

            msg += ("             Classe: PathHeap\n");
            msg += ("             Método: Remove\n");
            msg += ("             Variáveis:\n");
            msg += ("                  - lengh      = " + length + "\n");
            
            return msg;
        }


    }
}
