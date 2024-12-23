import tkinter as tk
from tkinter import messagebox

class PersistentSegmentTree:
    def __init__(self, size, initial_char=' '):
        self.size = size
        self.initial_char = initial_char
        self.roots = [self.build(0, size - 1)]

    class Node:
        def __init__(self, left=None, right=None, value=' '):
            self.left = left
            self.right = right
            self.value = value

    def build(self, l, r):
        if l == r:
            return self.Node(value=self.initial_char)
        mid = (l + r) // 2
        left_child = self.build(l, mid)
        right_child = self.build(mid + 1, r)
        return self.Node(left_child, right_child)

    def update(self, prev_node, l, r, idx, val):
        if l == r:
            return self.Node(value=val)
        mid = (l + r) // 2
        if idx <= mid:
            new_left = self.update(prev_node.left, l, mid, idx, val)
            return self.Node(new_left, prev_node.right)
        else:
            new_right = self.update(prev_node.right, mid + 1, r, idx, val)
            return self.Node(prev_node.left, new_right)

    def query(self, node, l, r, idx):
        if l == r:
            return node.value
        mid = (l + r) // 2
        if idx <= mid:
            return self.query(node.left, l, mid, idx)
        else:
            return self.query(node.right, mid + 1, r, idx)

    def get_text(self, node, l, r, result):
        if l == r:
            result.append(node.value)
            return
        mid = (l + r) // 2
        self.get_text(node.left, l, mid, result)
        self.get_text(node.right, mid + 1, r, result)

class UndoRedoTextEditor:
    def __init__(self, master, max_length=100):
        self.master = master
        self.master.title("Undo/Redo Text Editor with Persistent Segment Tree")
        self.max_length = max_length
        self.pst = PersistentSegmentTree(max_length)
        self.current_version = 0

        self.create_widgets()
        self.refresh_text()

    def create_widgets(self):
        frame = tk.Frame(self.master)
        frame.pack(padx=10, pady=10)

        # Text widget
        self.text = tk.Text(frame, width=60, height=20, wrap='word')
        self.text.grid(row=0, column=0, columnspan=4)
        self.text.bind("<KeyRelease>", self.on_key_release)

        # Undo/Redo buttons
        self.undo_button = tk.Button(frame, text="Undo", command=self.undo, state=tk.DISABLED)
        self.undo_button.grid(row=1, column=0, pady=5, sticky='ew')

        self.redo_button = tk.Button(frame, text="Redo", command=self.redo, state=tk.DISABLED)
        self.redo_button.grid(row=1, column=1, pady=5, sticky='ew')

        self.reset_button = tk.Button(frame, text="Reset", command=self.reset)
        self.reset_button.grid(row=1, column=2, pady=5, sticky='ew')

        self.quit_button = tk.Button(frame, text="Quit", command=self.master.quit)
        self.quit_button.grid(row=1, column=3, pady=5, sticky='ew')

    def on_key_release(self, event):
        # Get the current text
        current_text = self.text.get("1.0", tk.END)[:-1]  # Remove the trailing newline
        if len(current_text) > self.max_length:
            messagebox.showwarning("Warning", f"Maximum length of {self.max_length} characters exceeded.")
            self.text.delete("1.0", tk.END)
            self.refresh_text()
            return

        # Update the Persistent Segment Tree
        prev_root = self.pst.roots[self.current_version]
        # Find the differences between previous version and current_text
        # For simplicity, assume that only one character is changed at a time
        differences = []
        for idx in range(min(len(current_text), self.max_length)):
            prev_char = self.pst.query(prev_root, 0, self.max_length - 1, idx)
            if idx >= len(current_text):
                current_char = self.pst.initial_char
            else:
                current_char = current_text[idx]
            if prev_char != current_char:
                differences.append((idx, current_char))
        # Handle deletions
        if len(current_text) < self.max_length:
            for idx in range(len(current_text), self.max_length):
                prev_char = self.pst.query(prev_root, 0, self.max_length - 1, idx)
                if prev_char != self.pst.initial_char:
                    differences.append((idx, self.pst.initial_char))
        # Apply differences
        new_root = prev_root
        for idx, char in differences:
            new_root = self.pst.update(new_root, 0, self.max_length - 1, idx, char)
        if differences:
            # If we are not at the latest version, discard all redo history
            if self.current_version < len(self.pst.roots) - 1:
                self.pst.roots = self.pst.roots[:self.current_version + 1]
            self.pst.roots.append(new_root)
            self.current_version += 1
            self.refresh_buttons()

    def undo(self):
        if self.current_version > 0:
            self.current_version -= 1
            self.refresh_text()
            self.refresh_buttons()
        else:
            messagebox.showinfo("Info", "Tidak ada lagi tindakan untuk Undo.")

    def redo(self):
        if self.current_version < len(self.pst.roots) - 1:
            self.current_version += 1
            self.refresh_text()
            self.refresh_buttons()
        else:
            messagebox.showinfo("Info", "Tidak ada lagi tindakan untuk Redo.")

    def reset(self):
        self.pst = PersistentSegmentTree(self.max_length)
        self.current_version = 0
        self.refresh_text()
        self.refresh_buttons()

    def refresh_text(self):
        current_root = self.pst.roots[self.current_version]
        result = []
        self.pst.get_text(current_root, 0, self.max_length - 1, result)
        current_text = ''.join(result).rstrip()
        self.text.delete("1.0", tk.END)
        self.text.insert(tk.END, current_text)

    def refresh_buttons(self):
        # Update button states
        if self.current_version == 0:
            self.undo_button.config(state=tk.DISABLED)
        else:
            self.undo_button.config(state=tk.NORMAL)

        if self.current_version == len(self.pst.roots) - 1:
            self.redo_button.config(state=tk.DISABLED)
        else:
            self.redo_button.config(state=tk.NORMAL)

if __name__ == "__main__":
    root = tk.Tk()
    app = UndoRedoTextEditor(root, max_length=100)
    root.mainloop()
