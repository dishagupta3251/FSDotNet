<template>
    <div class="dashboard">
        <div v-for="(section, index) in sections" :key="index" class="section">
            <h2 @click="toggleSection(index)" @keydown.enter="toggleSection(index)" role="button" tabindex="0"
                class="section-title">
                {{ section.title }}
            </h2>
            <transition name="fade">
                <div v-if="section.isOpen" class="section-content">
                    <p>{{ section.content }}</p>
                </div>
            </transition>
        </div>
    </div>
</template>

<script>
export default {
    data() {
        return {
            sections: [
                { title: 'Section 1', content: 'Content for section 1', isOpen: false },
                { title: 'Section 2', content: 'Content for section 2', isOpen: false },
                { title: 'Section 3', content: 'Content for section 3', isOpen: false },
            ],
        };
    },
    methods: {
        toggleSection(index) {
            this.sections[index].isOpen = !this.sections[index].isOpen;
        },
    },
};
</script>

<style scoped>
.dashboard {
    padding: 20px;
}

.section {
    margin-bottom: 10px;
    border: 1px solid #ccc;
    border-radius: 5px;
}

.section-title {
    cursor: pointer;
    background-color: #f0f0f0;
    padding: 10px;
    border-bottom: 1px solid #ccc;
}

.section-content {
    padding: 10px;
}

.fade-enter-active,
.fade-leave-active {
    transition: opacity 0.5s;
}

.fade-enter,
.fade-leave-to {
    opacity: 0;
}
</style>