﻿namespace FamilyRegistration.Core.Decorator;
public abstract class AbstractScoreCalculator
{
    public abstract Task Execute(FamilyRegistrationContext context);
}