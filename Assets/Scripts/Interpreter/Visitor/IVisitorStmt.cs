   using UnityEngine;
   using System;
   using System.Collections;
   using System.Collections.Generic;
   using System.Linq;
    public interface IVisitorStmt<T>
    {
        T VisitExpressionStmt(ExpressionStmt stmt);
        T VisitPrintStmt(Print stmt);
        T VisitVarStmt(Var stmt);
        T VisitWhileStmt(While stmt);
        T VisitForStmt(For stmt);
        T VisitBlockStmt(Block stmt);

        T VisitEffectStmt(EffectStmt stmt);
        T VisitCardStmt(CardStmt stmt);
    }
