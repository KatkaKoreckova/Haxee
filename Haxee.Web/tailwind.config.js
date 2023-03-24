module.exports = {
    content: ["./**/*.{razor,html,cshtml}"],
    darkMode: 'class',
    theme: {
        screens: {
            sm: '480px',
            md: '720px',
            lg: '976px',
            xl: '1440px'
        },
        extend: {
            colors: {
                mainPurple: 'hsl(288, 43%, 34%)',
                lightPurple: 'hsl(294, 37%, 90%)',
                mainGreen: 'hsl(154, 57%, 49%)',
                mainBlue: 'hsl(188, 100%, 36%)',
                darkBlue: 'hsl(189, 100%, 15%)',
                darkBlueHover: 'hsl(189, 100%, 10%)',
                goldBg: 'hsl(45, 100%, 65%)',
                goldMain: 'hsl(45, 100%, 50%)',
                bronzeMain: 'hsl(30, 50%, 50%)',
                bronzeBg: 'hsl(30, 50%, 60%)',
                silverMain: 'hsl(0, 0%, 50%)',
                silverBg: 'hsl(0, 0%, 60%)',
                cardDefault: 'hsl(0, 0%, 85%)',
                yellow: 'hsl(51, 95%, 49%)',
                orange: 'hsl(21, 100%, 55%)',
                blue: 'hsl(183, 100%, 35%)',
                mint: 'hsl(171, 26%, 66%)'
            }
        },
    },
    plugins: [],
}