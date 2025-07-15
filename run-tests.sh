#!/bin/bash
# filepath: /Users/avinashremala/Desktop/AvinashTesting/run-tests.sh

echo "🚀 Avinash Testing - Complete Test Execution"
echo "============================================="

# Clean previous results
echo "🧹 Cleaning previous results..."
rm -rf TestResults/Screenshots/*
rm -f TestResults/*.trx
rm -f TestResults/*.html

# Build project
echo "🔨 Building project..."
dotnet build

if [ $? -eq 0 ]; then
    echo "✅ Build successful!"
    
    # Run tests
    echo "🧪 Running tests..."
    TIMESTAMP=$(date +"%Y%m%d_%H%M%S")
    dotnet test \
        --logger "trx;LogFileName=TestResults_${TIMESTAMP}.trx" \
        --logger "console;verbosity=normal" \
        --results-directory ./TestResults
    
    # Generate HTML report
    echo "📊 Generating HTML report..."
    dotnet run -- generate-report
    
    echo ""
    echo "🎉 Test execution completed!"
    echo "📁 Check TestResults directory for reports and screenshots"
else
    echo "❌ Build failed!"
    exit 1
fi