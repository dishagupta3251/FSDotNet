<template>
    <div class="unauthorized-page">
        <div class="lock"></div>
        <div class="message">
            <h1>Access to this page is restricted</h1>
        </div>
    </div>
</template>

<script>
export default {
    name: "UnauthorizedPage",
};
</script>

<style scoped lang="scss">
@import url('https://fonts.googleapis.com/css?family=Lato');

// Variables
$animationTime: 2s;

// Global Styles
* {
    position: relative;
    margin: 0;
    padding: 0;
    box-sizing: border-box;
    font-family: 'Lato', sans-serif;
}

.unauthorized-page {
    height: 100vh;
    display: flex;
    flex-direction: column;
    justify-content: center;
    align-items: center;
    background: linear-gradient(to bottom right, #EEE, #AAA);

    h1 {
        margin: 40px 0 20px;
    }

    .lock {
        border-radius: 13px;
        width: 55px;
        height: 45px;
        background-color: #333;
        animation: dip 2s;
        animation-delay: calc(#{$animationTime} - 0.5s);

        &::before,
        &::after {
            content: '';
            position: absolute;
            border-left: 5px solid #333;
            height: 20px;
            width: 15px;
            left: calc(50% - 9.5px);
        }

        &::before {
            top: -30px;
            border: 5px solid #333;
            border-bottom-color: transparent;
            border-radius: 15px 15px 0 0;
            height: 35px;
            animation: lock #{$animationTime},
            spin #{$animationTime};
        }

        &::after {
            top: -10px;
            border-right: 5px solid transparent;
            animation: spin #{$animationTime};
        }
    }
}

// Keyframes
@keyframes lock {
    0% {
        top: -45px;
    }

    65% {
        top: -45px;
    }

    100% {
        top: -30px;
    }
}

@keyframes spin {
    0% {
        transform: scaleX(-1);
        left: calc(50% - 30px);
    }

    65% {
        transform: scaleX(1);
        left: calc(50% - 12.5px);
    }
}

@keyframes dip {
    0% {
        transform: translateY(0px);
    }

    50% {
        transform: translateY(10px);
    }

    100% {
        transform: translateY(0px);
    }
}
</style>