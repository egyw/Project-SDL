import tkinter as tk
from tkinter import messagebox
import matplotlib.pyplot as plt
import networkx as nx

class TreeNode:
    def __init__(self, value, version):
        self.value = value
        self.version = version
        self.left = None  # Reference to parent node
        self.right = None  # Reference to child node

class PersistentTextEditor:
    def __init__(self, root):
        self.root = root
        self.root.title("Persistent Text Editor")
        
        # Persistent Tree variables
        self.tree_root = TreeNode("", 0)
        self.current_node = self.tree_root
        self.all_nodes = [self.tree_root]
        self.current_version = 0

        # UI Components
        self.textbox = tk.Text(self.root, wrap=tk.WORD, height=10, width=50)
        self.textbox.pack(padx=10, pady=10)

        self.button_frame = tk.Frame(self.root)
        self.button_frame.pack(padx=10, pady=5)

        self.btn_undo = tk.Button(self.button_frame, text="Undo", command=self.undo)
        self.btn_undo.pack(side=tk.LEFT, padx=5)

        self.btn_redo = tk.Button(self.button_frame, text="Redo", command=self.redo)
        self.btn_redo.pack(side=tk.LEFT, padx=5)

        self.btn_visualize = tk.Button(self.button_frame, text="Visualize Tree", command=self.visualize_tree)
        self.btn_visualize.pack(side=tk.LEFT, padx=5)

        self.textbox.bind("<KeyRelease>", self.on_text_change)

    def add_version(self, text):
    # Avoid adding a new version if the text is the same as the current node
        if self.current_node.value == text:
            return

        # Create a new node for the new version
        new_node = TreeNode(text, len(self.all_nodes))
        new_node.left = self.current_node  # Link to the current node (parent)

        # If we are branching off from a previous version (Undo + new text)
        if self.current_node.right is not None:
            self.current_node.right = None  # Break the old link to maintain branch

        # Link the new node as the right child
        self.current_node.right = new_node

        self.all_nodes.append(new_node)
        self.current_node = new_node
        self.current_version = new_node.version


    def undo(self):
        if self.current_node.left:
            self.current_node = self.current_node.left
            self.current_version = self.current_node.version
            self.textbox.delete("1.0", tk.END)
            self.textbox.insert(tk.END, self.current_node.value)
        else:
            messagebox.showinfo("Undo", "No more undo actions available!")

    def redo(self):
        if self.current_node.right:
            self.current_node = self.current_node.right
            self.current_version = self.current_node.version
            self.textbox.delete("1.0", tk.END)
            self.textbox.insert(tk.END, self.current_node.value)
        else:
            messagebox.showinfo("Redo", "No more redo actions available!")

    def on_text_change(self, event=None):
        self.add_version(self.textbox.get("1.0", tk.END).strip())

    def visualize_tree(self):
        G = nx.DiGraph()
        node_labels = {}

        # Add nodes and edges to the graph
        for node in self.all_nodes:
            G.add_node(node.version)
            node_labels[node.version] = f"{node.value} (v{node.version})"
            if node.left:
                G.add_edge(node.left.version, node.version)

        # Plot the tree using NetworkX's spring layout
        pos = nx.spring_layout(G)  # Alternative layout to graphviz
        plt.figure(figsize=(12, 8))
        nx.draw(G, pos, with_labels=True, labels=node_labels, node_size=3000, 
                node_color="skyblue", font_size=10, font_weight="bold", edge_color="gray")
        plt.title("Persistent Tree Visualization")
        plt.show()

if __name__ == "__main__":
    root = tk.Tk()
    app = PersistentTextEditor(root)
    root.mainloop()
