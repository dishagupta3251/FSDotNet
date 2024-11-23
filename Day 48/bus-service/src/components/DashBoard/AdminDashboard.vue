<template>

    <div class="sidebar-div">
        <!-- Sidebar -->
        <nav :class="['sidebar', { 'collapsed': isCollapsed }]">
            <div class="sidebar-header">
                <span class="brand" v-if="!isCollapsed">BusTicketing</span>
                <button class="toggle-btn" @click="toggleSidebar">
                    <i :class="isCollapsed ? 'fas fa-bars' : 'fas fa-times'"></i>
                </button>
            </div>
            <ul class="menu">
                <li class="menu-item" v-for="item in menuItems" :key="item.text">
                    <i :class="item.icon" class="menu-icon"></i>
                    <span class="menu-text" v-if="!isCollapsed">{{ item.text }}</span>
                </li>
            </ul>
            <div class="footer" v-if="!isCollapsed">
                <span>const Genius</span>
                <span>Web Developer</span>
            </div>
        </nav>

        <!-- Main Content -->
        <main class="content" :class="{ 'content-collapsed': isCollapsed }">
            <div class="row">
                <div class="col-sm-6">
                    <div class="card">
                        <div class="card-body">
                            <h5 class="card-title">Customers</h5>
                            <p class="card-text">200
                            </p>

                        </div>
                    </div>
                </div>
                <div class="col-sm-6">
                    <div class="card">
                        <div class="card-body">
                            <h5 class="card-title">Bus Operator</h5>
                            <p class="card-text">150
                            </p>

                        </div>
                    </div>
                </div>
            </div>
        </main>
    </div>
</template>

<script>
export default {
    data() {
        return {
            isCollapsed: false, // Sidebar state
            menuItems: [
                { icon: "fas fa-home", text: "Dashboard" },
                { icon: "fas fa-user", text: "User" },
                { icon: "fas fa-comments", text: "Messages" },
                { icon: "fas fa-chart-line", text: "Analytics" },
                { icon: "fas fa-folder", text: "Files" },
                { icon: "fas fa-shopping-cart", text: "Order" },
                { icon: "fas fa-heart", text: "Saved" },
                { icon: "fas fa-cog", text: "Settings" },
            ],
        };
    },
    methods: {
        toggleSidebar() {
            this.isCollapsed = !this.isCollapsed; // Toggle sidebar state
        },

    },
};
</script>

<style scoped>
/* General Layout */
.sidebar-div {
    display: flex;
    height: 100vh;
}

/* Sidebar */
.sidebar {
    background: rgb(19 68 50);
    color: #fff;
    width: 250px;
    height: 100vh;
    display: flex;
    flex-direction: column;
    position: relative;
    transition: width 0.3s ease, box-shadow 0.3s ease;
    box-shadow: 2px 0 8px rgba(0, 0, 0, 0.1);
    font-family: 'Poppins', sans-serif;
}

.sidebar.collapsed {
    width: 80px;
}

.sidebar-header {
    display: flex;
    justify-content: space-between;
    align-items: center;
    padding: 20px;
    border-bottom: 1px solid rgba(255, 255, 255, 0.1);
}

.brand {
    font-size: 1.3rem;
    font-weight: bold;
    letter-spacing: 0.5px;
    text-transform: uppercase;
}

.toggle-btn {
    background: none;
    border: none;
    color: #fff;
    font-size: 1.5rem;
    cursor: pointer;
}

.menu {
    list-style: none;
    padding: 0;
    margin: 0;
    flex-grow: 1;
}

.menu-item {
    display: flex;
    align-items: center;
    padding: 15px 20px;
    font-size: 0.95rem;
    color: rgba(255, 255, 255, 0.9);
    transition: background-color 0.3s ease, color 0.3s ease;
    cursor: pointer;
}

.menu-item:hover {
    background-color: rgba(255, 255, 255, 0.2);
}

.menu-icon {
    font-size: 1.3rem;
    margin-right: 15px;
    color: rgba(255, 255, 255, 0.9);
}

.menu-text {
    font-weight: 500;
}

.sidebar.collapsed .menu-text,
.sidebar.collapsed .brand {
    display: none;
}

/* Footer */
.footer {
    padding: 15px;
    text-align: center;
    font-size: 0.8rem;
    border-top: 1px solid rgba(255, 255, 255, 0.1);
}

/* Content Area */
.content {
    margin-left: 250px;
    padding: 20px;
    width: calc(100% - 250px);
    transition: margin-left 0.3s ease, width 0.3s ease;
    font-family: 'Poppins', sans-serif;
}

.content-collapsed {
    margin-left: 80px;
    width: calc(100% - 80px);
}

h1 {
    font-weight: 600;
    color: #333;
}

p {
    color: #555;
}

/* Responsive Design */
@media (max-width: 768px) {
    .sidebar {
        position: absolute;
        z-index: 1000;
        width: 250px;
        height: 100vh;
        transform: translateX(-100%);
        transition: transform 0.3s ease-in-out;
    }

    .sidebar.collapsed {
        transform: translateX(0);
        width: 80px;
    }

    .content {
        margin-left: 0;
        width: 100%;
    }

    .content-collapsed {
        margin-left: 80px;
    }
}
</style>