window.onCardReceived = async (reference, cardId) => {
    await reference.invokeMethodAsync('OnCardRead', cardId)
}

window.initializeScanning = async (dotNetReference) => {
    try {
        const ndef = new NDEFReader();
        await ndef.scan();
        console.log("> Scan started");

        ndef.addEventListener("readingerror", () => {
            alert("Argh! Cannot read data from the NFC tag. Try another one?");
        });

        ndef.addEventListener("reading", async ({ serialNumber }) => {
            console.log(`> Serial Number: ${serialNumber}`);
            await onCardReceived(dotNetReference, serialNumber)
        });
    } catch (error) {
        alert("Argh! Is your NFC enabled?");
    }
}