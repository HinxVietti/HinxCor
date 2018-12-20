using System.Collections.Generic;


public interface IOperationHistory
{
    List<IOperation> History { get; set; }
    void Undo();
    void Redo();
    void Do(IOperation cmd);
    bool CodRedo { get; set; }
    bool CodUndo { get; set; }
}

