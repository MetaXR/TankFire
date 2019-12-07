using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MirzaBeig.VFX;

public class FeiYiFireCtr : WDXFireCtr {
    public ParticleSystems FXFlow1;
    public ParticleSystems FXFlow2;    

    public new void StepAnim6()
    {
        FXFlow1.stop();
    }

    public new void StepAnim7()
    {
        FXFlow2.stop();
    }
    public new void StepAnim8()
    { }

}
