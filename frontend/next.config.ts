import type { NextConfig } from "next";

const nextConfig: NextConfig = {
  onDemandEntries: {
    // Period (in ms) where the server will keep pages in the buffer
    maxInactiveAge: 10 * 1000,
    // Number of pages that should be kept simultaneously without being disposed
    pagesBufferLength: 1,
  },
};

export default nextConfig;
