let reference

window.onCardReceived = async (cardId) => {
    await reference.invokeMethodAsync('OnCardRead', cardId)
}

window.initializeStandPage = (dotNetReference) => {
    reference = dotNetReference
}