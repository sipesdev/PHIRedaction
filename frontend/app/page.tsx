'use client'; // Use client-side rendering

import Image from "next/image";
import { useState } from "react";

export default function Home() {
  const [error, setError] = useState<string | null>(null);
  const [processing, setProcessing] = useState(false);

  // Function to handle file upload
  const handleFileUpload = async (event: React.FormEvent<HTMLFormElement>) => {
    event.preventDefault(); // Prevent default form behavior
    setError(null); // Reset error state
    setProcessing(true); // Set processing state
    const formData = new FormData(event.currentTarget);
    
    try {
      // Send the file to the backend
      const response = await fetch("http://localhost:5128/api/File/upload", {
        method: "POST",
        body: formData,
      });

      // Handle the response
      if (response.ok) {
        // Get the file name from the content disposition header
        const contentDisposition = response.headers.get("content-disposition");
        let fileName = "example_redacted.txt"; // Default file name
        
        // Extract the file name from the content disposition header
        if (contentDisposition) {
          const fileNameMatch = contentDisposition.match(/filename="?(.+)"?/i);
          if (fileNameMatch && fileNameMatch.length > 1) {
            fileName = fileNameMatch[0];
          }
        }

        // Get the response body as a blob
        const blob = await response.blob();

        // Create a temp URL for the blob
        const url = window.URL.createObjectURL(blob);

        // Create a temporary anchor element to trigger the download
        const a = document.createElement("a");
        a.href = url;
        a.download = fileName;
        document.body.appendChild(a);
        a.click(); // Trigger the download

        // Clean up
        a.remove();
        window.URL.revokeObjectURL(url);

        console.log("File processed and being sent to client.");

      } else {
        console.error("File upload failed:", response.statusText);
        setError(`File upload failed with status: ${response.statusText}`);
      }
    } catch (ex) {
      console.error("An unexpected error occured:", ex);
      setError(`${ex}`);
    } finally {
      setProcessing(false); // Reset processing state
    }
  }

  return (
    <div className="grid grid-rows-[auto_1fr_auto] items-center min-h-screen p-8 pb-20 gap-16 sm:p-20 font-[family-name:var(--font-geist-sans)]">
      <main className="flex flex-col gap-[32px] row-start-2 items-center mx-auto w-full max-w-4xl">
        <h1 className="text-[32px] leading-[40px] font-bold">
          PHI Redaction App
        </h1>
        <div className="flex gap-4 items-center flex-col sm:flex-row w-full justify-center">
          {/* Upload form */}
          <form
            onSubmit={handleFileUpload}
            encType="multipart/form-data"
            className="flex flex-col gap-4 w-full sm:w-auto">
            {/* Include file */}
            <input type="file" name="file" accept=".txt" required className="border p-2 rounded w-full"/>

            {/* Upload button */}
            <button type="submit" className="bg-blue-500 hover:bg-blue-700 text-white font-bold py-2 px-4 rounded">
              Redact Information
            </button>
          </form>
        </div>

        {/* Error message container */}
        <div className="min-h-[110px]">
          {error && (
            <div className="flex flex-col mt-4 p-4 bg-red-100 text-red-700 border border-red-400 rounded w-full">
              <p className="font-semibold">An error has occurred!</p>
              <p>{error}</p>
            </div>
          )}
        </div>
      </main>
      <footer className="row-start-3 flex gap-[24px] flex-wrap items-center justify-center mx-auto">
        Hello Invene! :)
      </footer>
    </div>
  );
}
